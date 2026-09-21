using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // Texto
    [SerializeField] private TextMeshProUGUI tiempoTexto;
    [SerializeField] private TextMeshProUGUI puntosTexto;

    // Vidas
    [SerializeField] private GameObject[] vidas;

    // Quite actualizar balas para poder controlar
    // la animación cuando recargas
    private gameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<gameManager>();

        // Suscribirse a los eventos
        gameManager.timer.OnTiempoCambiado += ActualizarTiempo;
        gameManager.puntos.OnPuntosCambiados += ActualizarPuntaje;
        gameManager.vidas.OnVidasCambiadas += ActualizarVidas;

        // Actualizar el estado inicial
        ActualizarTiempo(gameManager.timer.getTime());
        ActualizarPuntaje(gameManager.puntos.Puntos);
        ActualizarVidas(gameManager.vidas.VidaActual);
    }

    private void OnDestroy()
    {
        if (gameManager == null)
        {
            return;
        }

        // Dejar de observar los eventos
        gameManager.timer.OnTiempoCambiado -= ActualizarTiempo;
        gameManager.puntos.OnPuntosCambiados -= ActualizarPuntaje;
        gameManager.vidas.OnVidasCambiadas -= ActualizarVidas;
    }

    private void ActualizarTiempo(string tiempo)
    {
        tiempoTexto.text = $"Tiempo: {tiempo}";
    }

    private void ActualizarPuntaje(int puntos)
    {
        puntosTexto.text = $"Puntos: {puntos}";
    }

    private void ActualizarVidas(int vidasActuales)
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < vidasActuales);
        }
    }
}