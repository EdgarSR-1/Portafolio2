using UnityEngine;

public class Puntaje : MonoBehaviour
{
    private int puntos;

    // que todos puedan leer el puntaje
    public int Puntos => puntos;

    public void AgregarPuntos(int puntosNum)
    {
        puntos += puntosNum;

        // Debug.Log($"Puntos: {puntos}");
    }
}
