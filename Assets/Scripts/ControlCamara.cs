using UnityEngine;
using UnityEngine.InputSystem;

public class ControlCamara : MonoBehaviour
{
    // para el movimiento
    [SerializeField] private float maxMoveSpeed = 8f;

    [SerializeField] private float deadZone = 0.1f;

    // bounds para la camara
    [SerializeField] private Vector2 minBounds;

    [SerializeField] private Vector2 maxBounds;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePos =
            Mouse.current.position.ReadValue();

        float mouseX =
            mousePos.x / Screen.width;

        float mouseY =
            mousePos.y / Screen.height;

        Vector2 mouseNormalized = new Vector2(
            mouseX * 2f - 1f,
            mouseY * 2f - 1f
        );

        // para que no se mueva la camara si el mouse esta muy cerca del centro de la pantalla
        if (mouseNormalized.magnitude < deadZone)
        {
            return;
        }

        // normalizado para que no se mueva mas rapido si el mouse esta en la esquina de la pantalla
        mouseNormalized =
            Vector2.ClampMagnitude(
                mouseNormalized,
                1f
            );

        Vector3 movement =
            mouseNormalized *
            maxMoveSpeed *
            Time.deltaTime;

        Vector3 newPosition =
            transform.position + movement;

        ClampCamera(ref newPosition);

        transform.position = newPosition;
    }

    // para que la camara no se salga de los bounds
    private void ClampCamera(ref Vector3 position)
    {
        float cameraHeight =
            cam.orthographicSize;

        float cameraWidth =
            cameraHeight * cam.aspect;

        position.x = Mathf.Clamp(
            position.x,
            minBounds.x + cameraWidth,
            maxBounds.x - cameraWidth
        );

        position.y = Mathf.Clamp(
            position.y,
            minBounds.y + cameraHeight,
            maxBounds.y - cameraHeight
        );

        position.z = transform.position.z;
    }
}