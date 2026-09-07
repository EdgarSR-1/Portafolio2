using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ControlCrosshair : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private RectTransform crosshair;

    private void Awake()
    {
        crosshair = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Cursor.visible = false;
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
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
