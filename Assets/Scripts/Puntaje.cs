using UnityEngine;
using System;

public class Puntaje : MonoBehaviour
{
    private int puntos;

    public int Puntos => puntos;

    public event Action<int> OnPuntosCambiados;

    public void AgregarPuntos(int puntosNum)
    {
        puntos += puntosNum;

        OnPuntosCambiados?.Invoke(puntos);
    }
}