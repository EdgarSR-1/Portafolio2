using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntuacionAltaTexto;
    [SerializeField] private TextMeshProUGUI mejorTiempoTexto;

    // private PersistenciaManager persistencia;

    private void Start()
    {
        // persistencia = FindFirstObjectByType<PersistenciaManager>();

        // puntuacionAltaTexto.text = $"Mejor Puntuacion: {persistencia.PuntajeAlto}";

        int puntuacionAlta = PlayerPrefs.GetInt("PuntosAlto", 0);
        float mejorTiempo = PlayerPrefs.GetFloat("MejorTiempo", 0f);

        int minutos = Mathf.FloorToInt(mejorTiempo / 60f);
        int segundos = Mathf.FloorToInt(mejorTiempo % 60);

        puntuacionAltaTexto.text = $"Mejor Puntuacion: {puntuacionAlta}";

        mejorTiempoTexto.text = $"Mejor Tiempo: {minutos:00}:{segundos:00}";
    }

    public void Iniciar()
    {
        SceneManager.LoadScene(1);
    }
}
