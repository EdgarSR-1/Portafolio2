using UnityEngine;

public class gameManager : MonoBehaviour
{
    // agregue esto para que cada parte del manager no tenga que buscar
    // separadamente cada otro manager
    public Vidas vidas { get; private set; }
    public Puntaje puntos { get; private set; }
    public Timer timer { get; private set; }
    public DisparoControl disparo { get; private set; }

    private void Awake()
    {
        vidas = GetComponent<Vidas>();
        puntos = GetComponent<Puntaje>();
        timer = GetComponent<Timer>();
        disparo = GetComponent<DisparoControl>();
    }
}
