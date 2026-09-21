using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private float tiempo;
    private bool isRunning;

    // para que otros scripts puedan solo ver el tiempo
    public float Tiempo => tiempo;

    private int ultimoSegundo;

    public event Action<string> OnTiempoCambiado;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (!isRunning) { return; }

        tiempo += Time.deltaTime;

        int segundoActual = Mathf.FloorToInt(tiempo);

        if (segundoActual != ultimoSegundo)
        {
            ultimoSegundo = segundoActual;
            OnTiempoCambiado?.Invoke(getTime());
        }
    }

    private void StartTimer()
    {
        tiempo = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public string getTime()
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);

        int segundos = Mathf.FloorToInt(tiempo % 60f);

        // para que siempre se vea con 2 dígitos: 01:05 en vez de 1:5
        return $"{minutos:00}:{segundos:00}";
    }
}
