using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    // referencias para daño al jugador
    [SerializeField] private SpriteRenderer spriteRenderer;
    // para obtener el segundo sprite
    [SerializeField] private Sprite spriteCaida;
    // [SerializeField] private Vidas vidas;
    // private Vidas vidas;
    private gameManager gameManager;
    
    // no se deberia de necesitar - borrar despues
    // [SerializeField] private Collider2D colliderEnemigo;

    // para el ataque al jugador
    [SerializeField] private float tiempoAtaque = 5f;
    [SerializeField] private float warningAtaque = 2f;

    // para cuando muere un enemigo
    [SerializeField] private float duracionCaida = 0.5f;
    // [SerializeField] private float anguloCaida = 90f;
    // private Puntaje puntosManager;
    [SerializeField] private int puntosDar = 100;

    // sonido
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoDisparo;

    private Collider2D enemigo;

    private bool isDead = false;

    private Color colorOrig;
    private float tiempoVivo;

    private void Awake()
    {
        enemigo = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colorOrig = spriteRenderer.color;

        // necesito esto porque el enemigo es un prefab y no me
        // deja agregarle el script que esta en gameManager
        // manualmente
        // vidas = FindFirstObjectByType<Vidas>();
        // puntosManager = FindFirstObjectByType<Puntaje>();
        gameManager = FindFirstObjectByType<gameManager>();
    }

    private void Update()
    {
        if(isDead) { return; }

        UpdateTimerAtaque();
    }

    private void UpdateTimerAtaque()
    {
        tiempoVivo += Time.deltaTime;

        float warningStart = tiempoAtaque - warningAtaque;

        if (tiempoVivo >= warningStart)
        {
            float warningProgreso =
                (tiempoVivo - warningStart) / warningAtaque;
            
            spriteRenderer.color = Color.Lerp(
                colorOrig, Color.red, warningProgreso
            );
        }

        if (tiempoVivo >= tiempoAtaque)
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        audioSource.PlayOneShot(sonidoDisparo);
        gameManager.vidas.TomarDaño();

        ResetTiempo();
    }

    private void ResetTiempo()
    {
        tiempoVivo = 0;
        spriteRenderer.color = colorOrig;
    }

    public void Hit()
    {
        if (isDead) { return; }

        isDead = true;

        enemigo.enabled = false;

        // puntosManager.AgregarPuntos(puntosDar);
        gameManager.puntos.AgregarPuntos(puntosDar);

        StartCoroutine(Muerte());
    }

    // private IEnumerator Muerte()
    // {
    //     Quaternion rotacionInicio = transform.rotation;
         
    //     Quaternion rotacionFinal = rotacionInicio * Quaternion.Euler(0f, 0f, anguloCaida);

    //     float tiempo = 0f;

    //     while (tiempo < duracionCaida)
    //     {
    //         tiempo += Time.deltaTime;

    //         float progreso = tiempo / duracionCaida;

    //         transform.rotation = Quaternion.Lerp(rotacionInicio, rotacionFinal, progreso);

    //         yield return null;
    //     }

    //     transform.rotation = rotacionFinal;

    //     Destroy(gameObject);
    // }

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
                posicionInicial, posicionFinal, progreso
            );

            yield return null;
        }

        transform.position = posicionFinal;

        Destroy(gameObject);
    }
}
