using UnityEngine;

public class SpawnOcupado : MonoBehaviour
{
    [Header("Parallax")]
    [SerializeField] private Transform parallaxParent;
    [Header("Sorting")]
    [SerializeField] private  int enemySortOrder = 10;

    private GameObject enemigo;

    // checa si un enemigo esta asignado a un obstaculo
    public bool IsOccupied => enemigo != null;
    
    // es para obtener el transform del objeto padre
    public Transform ParallaxParent => transform.parent;

    // para poder saber el orden en que se tiene que poner
    // el enemigo y se pueda dibujar detras de obstaculos que
    // estan en diferentes order in layer
    public int sort => enemySortOrder;

    public void Ocupado(GameObject newEnemigo)
    {
        enemigo = newEnemigo;
    }

    public void Release()
    {
        enemigo = null;
    }
}
