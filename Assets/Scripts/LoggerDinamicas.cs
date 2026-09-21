using UnityEngine;
using System.IO;

public class LoggerDinamicas : MonoBehaviour
{
    private int disparos;
    private int recargar;
    private int enemigosEliminados;

    public void RegistrarDisparo()
    {
        disparos++;
    }

    public void RegistrarEnemigoEliminado()
    {
        enemigosEliminados++;
    }

    public void RegistrarRecarga()
    {
        recargar++;
    }

    public void ExportarCSV(float tiempo)
    {
        string ruta = Path.Combine(Application.dataPath, "dinamicas.csv");

        string contenido =
            "Tiempo,Disparos,Recargar,EnemigosEliminados\n" +
            $"{tiempo:F2},{disparos},{recargar},{enemigosEliminados}";

        File.WriteAllText(ruta, contenido);

        Debug.Log("Datos exportados a" + ruta);
    }
}
