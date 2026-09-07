using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoControl : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;

    // sistema de balas
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private float tiempoRecarga = 1.5f;

    private int municionActual;
    private bool isReloading;

    private void Start()
    {
        municionActual = maxAmmo;
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            tryDisparo();
        }
    }

    // private void Disparo()
    // {
    //     // para sacar posicion de mouse
    //     Vector2 mousePos = Mouse.current.position.ReadValue();

    //     // para obtener posicion en el mapa
    //     Vector3 mundoPos = gameCamera.ScreenToWorldPoint(
    //         new Vector3(mousePos.x, mousePos.y, -gameCamera.transform.position.z)
    //     );

    //     Collider2D hit = Physics2D.OverlapPoint(mundoPos);

    //     if (hit != null)
    //     {
    //         Target target = hit.GetComponent<Target>();
            
    //         if (target != null)
    //         {
    //             target.Hit();
    //         }
    //     }
    // }

    private void tryDisparo()
    {
        if (isReloading) return;

        if (municionActual <= 0)
        {
            StartCoroutine(Recarga());
            return;
        }

        municionActual--;
        Debug.Log($"Municion: {municionActual}/{maxAmmo}");

        Disparo();
    }

    private void Disparo()
    {
        // para sacar posicion de mouse
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // para obtener posicion en el mapa
        Vector3 mundoPos = gameCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, -gameCamera.transform.position.z)
        );

        Collider2D[] hits = Physics2D.OverlapPointAll(mundoPos);

        if (hits.Length == 0)
        {
            Debug.Log("Fallo");
        }

        Collider2D frenteMax = null;

        int sortMax = int.MinValue;

        foreach (Collider2D hit in hits)
        {
            SpriteRenderer spriteRenderer =
                hit.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null){ continue; }

            if (spriteRenderer.sortingOrder > sortMax)
            {
                sortMax = spriteRenderer.sortingOrder;

                frenteMax = hit;
            } 
        }

        if (frenteMax == null)
        {
            Debug.Log("Fallo");
            return;
        }

        Target target = frenteMax.GetComponent<Target>();

        if (target != null)
        {
            target.Hit();
        }
        else
        {
            Debug.Log("Bloqueado por: " + frenteMax.name);
        }
    }

    private IEnumerator Recarga()
    {
        if(isReloading) yield break;

        isReloading = true;

        Debug.Log("Recargando");

        yield return new WaitForSeconds(tiempoRecarga);

        municionActual = maxAmmo;

        isReloading = false;

        Debug.Log($"Recarga completa. Municion: {municionActual}/{maxAmmo}");
    }
}
