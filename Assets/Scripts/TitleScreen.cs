using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntuacionAltaTexto;
    [SerializeField] private TextMeshProUGUI mejorTiempoTexto;

    // panel de instrucciones
    [SerializeField] private GameObject panelInstrucciones;

    // panel de creditos
    [SerializeField] private GameObject panelCreditos;

    // private PersistenciaManager persistencia;

    private void Start()
    {
        panelInstrucciones.SetActive(false);
        panelCreditos.SetActive(false);
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

    public void AbrirInstrucciones()
    {
        panelInstrucciones.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        panelInstrucciones.SetActive(false);
    }

    public void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }
}
