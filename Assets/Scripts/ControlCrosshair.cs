using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlCrosshair : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    // animación de rotación
    [SerializeField] private float rotacionPorDisparo = 60f;
    [SerializeField] private float rotacionVel = 180f;
    // [SerializeField] private float recargaRotacion = 360f;


    private RectTransform crosshair;
    private float rotacionTarget;
    private Coroutine rotacionCoroutine;

    private void Awake()
    {
        crosshair = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Cursor.visible = false;
        rotacionTarget = 0f;
    }

    private void Update()
    {
        if (Mouse.current == null) { return; }

        Vector2 mousePos = Mouse.current.position.ReadValue();

        RectTransform canvasRect = canvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            canvas.worldCamera,
            out Vector2 localPosition))
        {
            crosshair.anchoredPosition = localPosition;
        }

        Rotar();
    }

    private void Rotar()
    {
        // se necesita porque el crosshair es un UI y no un objeto del mundo, no puedo poner transform.Rotate
        // localEulerAngles significa que se rota en el espacio local del objeto, no en el espacio global del mundo
        float rotacionActual = crosshair.localEulerAngles.z;

        float rotacionNueva = Mathf.MoveTowardsAngle(
            rotacionActual, rotacionTarget, rotacionVel * Time.deltaTime
        );

        crosshair.localRotation = Quaternion.Euler(0f, 0f, rotacionNueva);
    }

    public void Disparo()
    {
        rotacionTarget += rotacionPorDisparo;
    }

    public void ReiniciarRotacion()
    {
        rotacionTarget = 0f;
        crosshair.localRotation = Quaternion.identity;
    }

    public void RecargaRotacion(float duracion)
    {
        StartCoroutine(AnimacionRecarga(duracion));
    }

    private IEnumerator AnimacionRecarga(float duracion)
    {
        float rotacionInicial = crosshair.localEulerAngles.z;
        float rotacionFinal = rotacionInicial + 360f;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            float progreso = Mathf.Clamp01(tiempo / duracion);

            float rotacion = Mathf.Lerp(
                rotacionInicial, rotacionFinal, progreso
            );

            crosshair.localRotation = Quaternion.Euler( 0f, 0f, rotacion );

            yield return null;
        }

        crosshair.localRotation = Quaternion.Euler( 0f,0f, rotacionFinal );

        rotacionTarget = rotacionFinal;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
