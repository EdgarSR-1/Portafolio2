using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoControl : MonoBehaviour
{

    private gameManager gameManager;

    [SerializeField] private Camera gameCamera;

    // sistema de balas
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private float tiempoRecarga = 1.5f;

    // animaciones
    [SerializeField] private ControlCrosshair crosshair;
    [SerializeField] private GameObject[] iconos;

    private int municionActual;

    // para que lo pueda leer mi UI
    public int MunicionActual => municionActual;

    // audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private AudioClip sonidoRecarga;

    private bool isReloading;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<gameManager>();
    }

    private void Start()
    {
        municionActual = maxAmmo;
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            tryDisparo();
        }
    }

    private void tryDisparo()
    {
        if (isReloading) return;

        if (municionActual <= 0)
        {
            StartCoroutine(Recarga());
            gameManager.logger.RegistrarRecarga();
            return;
        }

        municionActual--;
        iconos[municionActual].SetActive(false);
        crosshair.Disparo();
        audioSource.PlayOneShot(sonidoDisparo);
        
        // Debug.Log($"Municion: {municionActual}/{maxAmmo}");

        gameManager.logger.RegistrarDisparo();
        Disparo();
    }

    private void Disparo()
    {
        // para sacar posicion de mouse
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // para obtener posicion en el mapa
        Vector3 mundoPos = gameCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, -gameCamera.transform.position.z)
        );

        Collider2D[] hits = Physics2D.OverlapPointAll(mundoPos);

        // if (hits.Length == 0)
        // {
        //     Debug.Log("Fallo");
        // }

        Collider2D frenteMax = null;

        int sortMax = int.MinValue;

        foreach (Collider2D hit in hits)
        {
            SpriteRenderer spriteRenderer =
                hit.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null){ continue; }

            if (spriteRenderer.sortingOrder > sortMax)
            {
                sortMax = spriteRenderer.sortingOrder;

                frenteMax = hit;
            } 
        }

        if (frenteMax == null)
        {
            // Debug.Log("Fallo");
            return;
        }

        Target target = frenteMax.GetComponent<Target>();

        if (target != null)
        {
            target.Hit();
        }
        // else
        // {
        //     Debug.Log("Bloqueado por: " + frenteMax.name);
        // }
    }

    private IEnumerator Recarga()
    {
        if(isReloading) yield break;

        isReloading = true;

        // Debug.Log("Recargando");

        float tiempoPorBala = tiempoRecarga / maxAmmo;

        crosshair.RecargaRotacion(tiempoRecarga);

        // yield return new WaitForSeconds(tiempoRecarga);

        for (int i = 0; i < maxAmmo; i ++)
        {
            yield return new WaitForSeconds(tiempoPorBala);
            iconos[i].SetActive(true);
            audioSource.PlayOneShot(sonidoRecarga);
        }

        municionActual = maxAmmo;

        isReloading = false;

        // Debug.Log($"Recarga completa. Municion: {municionActual}/{maxAmmo}");
    }
}
