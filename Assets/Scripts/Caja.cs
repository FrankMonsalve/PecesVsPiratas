using UnityEngine;

public class Caja : MonoBehaviour
{
    private bool isOcupada = false; // Verifica si la caja est� ocupada
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CambiarColorCaja(Color.green); // Comienza con color verde, ya que est� libre
    }

    // M�todo para verificar si la caja est� ocupada
    public bool IsCajaOcupada()
    {
        return isOcupada;
    }

    // M�todo para marcar la caja como ocupada
    public void MarcarCajaOcupada()
    {
        isOcupada = true;
        CambiarColorCaja(Color.red); // Caja ocupada, color rojo
    }

    // M�todo para marcar la caja como libre
    public void MarcarCajaLibre()
    {
        isOcupada = false;
        CambiarColorCaja(Color.green); // Caja libre, color verde
    }

    // M�todo para cambiar el color de la caja
    private void CambiarColorCaja(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}

public class PrefabMover : MonoBehaviour
{
    private SpriteRenderer prefabRenderer;
    private bool cercaDeCaja = false; // Determina si el prefab est� cerca de una caja
    private Caja cajaDetectada;

    void Start()
    {
        prefabRenderer = GetComponent<SpriteRenderer>();
        prefabRenderer.color = Color.white; // El prefab comienza blanco
    }

    // Detecta cuando el prefab entra en la zona de la caja
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("boxUp")) // Comprobamos si el prefab entra en contacto con una caja
        {
            cajaDetectada = col.GetComponent<Caja>(); // Obtenemos la referencia a la caja
            cercaDeCaja = true;

            // Si la caja no est� ocupada
            if (!cajaDetectada.IsCajaOcupada())
            {
                // Colocamos el prefab dentro de la caja
                transform.position = cajaDetectada.transform.position; // Lo colocamos en la posici�n de la caja
                cajaDetectada.MarcarCajaOcupada();  // Marcamos la caja como ocupada
                prefabRenderer.color = Color.green; // El prefab se vuelve verde al estar dentro de la caja
            }
            else
            {
                // Si la caja est� ocupada, mantenemos el color blanco del prefab
                prefabRenderer.color = Color.white; // El prefab no puede entrar, permanece blanco
            }
        }
    }

    // Detecta cuando el prefab sale de la zona de la caja
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("boxUp"))
        {
            cercaDeCaja = false;

            // Si el prefab sale, volvemos al estado inicial (blanco)
            if (!cajaDetectada.IsCajaOcupada())
            {
                prefabRenderer.color = Color.white; // Color blanco si el prefab no est� dentro de la caja
            }
        }
    }
}
