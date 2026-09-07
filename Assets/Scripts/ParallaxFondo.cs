using UnityEngine;

public class ParallaxFondo : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Range(0f, 2f)]
    [SerializeField] private float parallaxMultx = 0.5f;
    [Range(0f, 2f)]
    [SerializeField] private float parallaxMulty = 0.5f;

    private Vector3 prevCamPos;

    private void Start()
    {
        prevCamPos = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - prevCamPos;

        transform.position += new Vector3(
            cameraDelta.x * parallaxMultx,
            cameraDelta.y * parallaxMulty,
            0f
        );   

        prevCamPos = cameraTransform.position;
    }
}
