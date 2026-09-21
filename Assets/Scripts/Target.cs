using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    // Referencias visuales
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteCaida;

    // Referencia a los sistemas principales
    private gameManager gameManager;

    // Configuración del ataque
    [SerializeField] private float tiempoAtaque = 5f;
    [SerializeField] private float warningAtaque = 2f;

    // Configuración de muerte
    [SerializeField] private float duracionCaida = 0.5f;

    // Puntos
    [SerializeField] private int puntosDar = 100;

    // Sonido
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoDisparo;

    // Collider
    private Collider2D enemigo;

    // Color original
    private Color colorOrig;

    // Estado actual
    private IEnemyState estadoActual;

    // Propiedades para los estados
    public float TiempoAtaque => tiempoAtaque;
    public float WarningAtaque => warningAtaque;
    public Color ColorOriginal => colorOrig;

    private void Awake()
    {
        enemigo = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colorOrig = spriteRenderer.color;

        // El enemigo es un prefab y se instancia durante la partida
        gameManager = FindFirstObjectByType<gameManager>();
    }

    private void Start()
    {
        ChangeState(new EnemyNormalState(this));
    }

    private void Update()
    {
        estadoActual?.Update();
    }

    public void ChangeState(IEnemyState nuevoEstado)
    {
        estadoActual?.Exit();

        estadoActual = nuevoEstado;

        estadoActual.Enter();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void Atacar()
    {
        audioSource.PlayOneShot(sonidoDisparo);

        gameManager.vidas.TomarDaño();
    }

    public void Hit()
    {
        if (estadoActual is EnemyDefeatState)
        {
            return;
        }

        enemigo.enabled = false;

        gameManager.puntos.AgregarPuntos(puntosDar);

        ChangeState(new EnemyDefeatState(this));
    }

    public void IniciarMuerte()
    {
        StartCoroutine(Muerte());
    }

    private IEnumerator Muerte()
    {
        spriteRenderer.sprite = spriteCaida;

        Vector3 posicionInicial = transform.position;
        Vector3 posicionFinal = posicionInicial + Vector3.down;

        float tiempo = 0f;

        while (tiempo < duracionCaida)
        {
            tiempo += Time.deltaTime;

            float progreso = tiempo / duracionCaida;

            transform.position = Vector3.Lerp(
                posicionInicial,
                posicionFinal,
                progreso
            );

            yield return null;
        }

        transform.position = posicionFinal;

        Destroy(gameObject);
    }
}