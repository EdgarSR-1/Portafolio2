using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // Texto
    [SerializeField] private TextMeshProUGUI tiempoTexto;
    [SerializeField] private TextMeshProUGUI puntosTexto;

    // Vidas
    [SerializeField] private GameObject[] vidas;

    // quite actualizar balas para poder controlar la animacion cuando recargas

    private gameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<gameManager>();
    }

    private void Update()
    {
        ActualizarTiempo();
        ActualizarPuntaje();
        ActualizarVidas();
    }

    private void ActualizarTiempo()
    {
        tiempoTexto.text = $"Tiempo: {gameManager.timer.getTime()}";
    }
    private void ActualizarPuntaje()
    {
        puntosTexto.text = $"Puntos: {gameManager.puntos.Puntos}";
    }

    private void ActualizarVidas()
    {
        int vidasActuales = gameManager.vidas.VidaActual;

        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < vidasActuales);
        }
    }
}
