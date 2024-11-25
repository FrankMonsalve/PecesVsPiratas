using UnityEngine;

public class Arrastre : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private Transform cajaPadre;
    private Vector3 posicionInicial;
    private Transform cajaDetectada; // Almacena la última caja detectada
    private SpriteRenderer spriteRenderer; // Para cambiar la apariencia del objeto mientras se arrastra

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Se hace cinemático para evitar la interferencia con la física durante el arrastre
        posicionInicial = transform.position;

        // Obtenemos el componente SpriteRenderer para cambiar la apariencia del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0) + offset;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Cambiar la apariencia mientras se arrastra (opcionalmente semi-transparente)
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Hacer el objeto semi-transparente
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Restaurar la apariencia después de soltar
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; // Restaurar el color original
        }

        if (cajaDetectada != null && !cajaDetectada.GetComponent<Caja>().IsCajaOcupada())
        {
            // Asigna y marca la caja como ocupada
            cajaPadre = cajaDetectada;
            cajaPadre.GetComponent<Caja>().MarcarCajaOcupada();
            transform.position = cajaPadre.position;
            Debug.Log("Objeto colocado correctamente en la caja.");
        }
        else
        {
            // Vuelve a la posición inicial si no está dentro de una caja válida
            VolverAPosicionInicial();
            Debug.Log("No se puede colocar el objeto en la caja.");
        }
    }

    public void VolverAPosicionInicial()
    {
        if (cajaPadre != null)
        {
            cajaPadre.GetComponent<Caja>().MarcarCajaLibre(); // Libera la caja si estaba asignada
            cajaPadre = null;
        }
        transform.position = posicionInicial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("boxUp"))
        {
            cajaDetectada = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("boxUp") && cajaDetectada == collision.transform)
        {
            cajaDetectada = null; // Solo borra si es la misma caja detectada
        }
    }

    public void ColocarEnCaja(Transform caja)
    {
        transform.position = caja.position; // Coloca el objeto dentro de la caja
    }

    public void SalirDeCaja()
    {
        Debug.Log("Objeto ha salido de la caja.");
        // Puedes agregar más lógica aquí si es necesario
    }
}
