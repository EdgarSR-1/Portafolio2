using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    // resultados
    [SerializeField] private TextMeshProUGUI puntaje;
    [SerializeField] private TextMeshProUGUI tiempo;

    // tener persistencia en mejor tiempo y puntos
    // private PersistenciaManager persistencia;
    // mejor uso solo playerprefs directo - BORRAR
    

    private Timer timer;
    private Puntaje puntosManager;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        puntosManager = GetComponent<Puntaje>();
        // persistencia = GetComponent<PersistenciaManager>();
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        timer.StopTimer();

        int puntosFinal = puntosManager.Puntos;
        float tiempoFinal = timer.Tiempo;

        // persistencia.GuardarPuntos(puntosFinal);
        // persistencia.GuardarTiempo(tiempoFinal);

        int puntosPrev = PlayerPrefs.GetInt("PuntosAlto", 0);

        if (puntosFinal > puntosPrev)
        {
            PlayerPrefs.SetInt("PuntosAlto", puntosFinal);
        }

        float tiempoPrev = PlayerPrefs.GetFloat("MejorTiempo", 0f);
        
        if (tiempoFinal > tiempoPrev)
        {
            PlayerPrefs.SetFloat("MejorTiempo", tiempoFinal);
        }

        // para mostrar puntos finales y tiempo que sobrevivio el jugador
        puntaje.text = $"Puntos: {puntosFinal}";
        tiempo.text = $"Tiempo: {timer.getTime()}";

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // use scenemanager para no tener que preocuparme por hacer una funcion de reiniciar por separado para todo
    public void Reintentar()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
