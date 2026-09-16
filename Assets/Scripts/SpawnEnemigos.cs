using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemigos : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private SpawnOcupado[] spawnPoints;

    // para dificultad
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float reduccion = 0.25f;
    [SerializeField] private float intervalMin = 0.5f;


    private float spawnTimer;
    private Timer gameTimer;

    private void Start()
    {
        gameTimer = FindFirstObjectByType<Timer>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        float intervaloActual = ObtenerSpawnInterval();

        if (spawnTimer >= intervaloActual)
        {
            EnemigoSpawn();
            spawnTimer = 0f;
        }
    }

    private float ObtenerSpawnInterval()
    {
        int nivelDificultad = Mathf.FloorToInt(gameTimer.Tiempo / 15f); // cada 15 segundos sube

        float intervalo = spawnInterval - (nivelDificultad * reduccion);

        return Mathf.Max(intervalo, intervalMin);
    }

    private void EnemigoSpawn()
    {
        List<SpawnOcupado> spawnDisponible = new List<SpawnOcupado>();

        foreach (SpawnOcupado puntoSpawn in spawnPoints)
        {
            if (!puntoSpawn.IsOccupied)
            {
                spawnDisponible.Add(puntoSpawn);
            }
        }

        if (spawnDisponible.Count == 0) { return; }

        int randomIndex = Random.Range(0, spawnDisponible.Count);

        SpawnOcupado seleccionado = spawnDisponible[randomIndex];

        GameObject enemigo = Instantiate(
            enemigoPrefab,
            seleccionado.ParallaxParent
        );

        enemigo.transform.localPosition = seleccionado.transform.localPosition;

        SpriteRenderer renderer = enemigo.GetComponent<SpriteRenderer>();

        renderer.sortingOrder = seleccionado.sort;

        seleccionado.Ocupado(enemigo);
    }
}
