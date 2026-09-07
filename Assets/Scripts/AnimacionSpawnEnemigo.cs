using UnityEngine;
using System.Collections;

public class AnimacionSpawnEnemigo : MonoBehaviour
{
    [Header("Animación para Spawn")]
    [SerializeField] private float distancia = 2f;
    [SerializeField] private float duracion = 0.5f;

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.position;

        transform.position = targetPos + Vector3.down * distancia;

        StartCoroutine(SpawnAnimacion());
    }

    private IEnumerator SpawnAnimacion()
    {
        Vector3 startPos = transform.position;

        float pasado = 0f;

        while (pasado < duracion)
        {
            pasado += Time.deltaTime;

            float progreso = pasado / duracion;

            transform.position = 
                Vector3.Lerp(
                    startPos,
                    targetPos,
                    progreso
                );

            yield return null;
        }

        transform.position = targetPos;
    }
}
