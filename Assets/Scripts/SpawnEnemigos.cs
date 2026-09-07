using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemigos : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private SpawnOcupado[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            EnemigoSpawn();

            spawnTimer = 0f;
        }
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
