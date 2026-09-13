using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // Texto
    [SerializeField] private TextMeshProUGUI tiempoTexto;
    [SerializeField] private TextMeshProUGUI puntosTexto;

    // Vidas
    [SerializeField] private GameObject[] vidas;

    // moví balas para poder controlar la animacion cuando recargas
    // Balas
    // [SerializeField] private GameObject[] balas;

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
        // ActualizarBalas();
    }

    private void ActualizarTiempo()
    {
        // tiempoTexto.text = gameManager.timer.getTime();
        tiempoTexto.text = $"Tiempo: {gameManager.timer.getTime()}";
    }
    private void ActualizarPuntaje()
    {
        // puntosTexto.text = gameManager.puntos.Puntos.ToString();
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

    // private void ActualizarBalas()
    // {
    //     int balasActuales = gameManager.disparo.MunicionActual;

    //     for (int i = 0; i < balas.Length; i++)
    //     {
    //         balas[i].SetActive(i < balasActuales);
    //     }
    // }
}
