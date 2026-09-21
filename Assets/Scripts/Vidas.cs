using UnityEngine;
using System;

public class Vidas : MonoBehaviour
{
    [SerializeField] private int vidasMax = 3;

    private GameOverManager gameOverManager;

    private int vidaActual;

    // para que otros scripts puedan ver la vida
    // pero no modificarla directamente
    public int VidaActual => vidaActual;


    public event Action<int> OnVidasCambiadas;

    private void Awake()
    {
        vidaActual = vidasMax;
        gameOverManager = GetComponent<GameOverManager>();

        OnVidasCambiadas?.Invoke(vidaActual);
    }

    public void TomarDaño()
    {
        vidaActual--; // si te disparan siempre pierdes 1 vida

        // para que la vida nunca sea negativa por si las dudas
        vidaActual = Mathf.Max(vidaActual, 0);

        OnVidasCambiadas?.Invoke(vidaActual);

        // Debug.Log($"Daño al jugador. Vidas: {vidaActual}/{vidasMax}");


        if (vidaActual <= 0)
        {
            gameOverManager.GameOver();
        }
    }
}
