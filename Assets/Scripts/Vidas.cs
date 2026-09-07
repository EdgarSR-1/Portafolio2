using UnityEngine;

public class Vidas : MonoBehaviour
{
    [SerializeField] private int vidasMax = 3;

    private int vidaActual;

    // para que otros scripts puedan ver la vida
    // pero no modificarla directamente
    public int VidaActual => vidaActual;

    private void Awake()
    {
        vidaActual = vidasMax;
    }

    public void TomarDaño()
    {
        vidaActual--; // si te disparan siempre pierdes 1 vida

        // para que la vida nunca sea negativa por si las dudas
        vidaActual = Mathf.Max(vidaActual, 0);

        Debug.Log($"Daño al jugador. Vidas: {vidaActual}/{vidasMax}");


        if (vidaActual <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
