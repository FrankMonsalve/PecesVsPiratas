using UnityEngine;

public class BoxZone2D : MonoBehaviour
{
    public GameObject currentObject = null; // El objeto que está dentro de esta caja
    private CajaManager cajaManager; // Referencia a CajaManager para actualizar las cajas abajo cuando se mueven objetos

    private void Start()
    {
        cajaManager = FindObjectOfType<CajaManager>(); // Buscar CajaManager
    }

    // Detectar cuando un objeto entra en la caja
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Solo procesamos si es un "Personaje"
        if (!other.CompareTag("Personaje")) return;

        // Si la caja ya tiene un objeto, no aceptar otro
        if (currentObject != null)
        {
            Debug.Log($"Caja ocupada. {other.name} no puede entrar.");
            ReturnToOriginalPosition(other.gameObject);
            return;
        }

        // Si la caja está vacía, poner el objeto dentro
        currentObject = other.gameObject;

        // Convertir el objeto en hijo de la caja y centrarlo
        other.transform.SetParent(transform);
        other.transform.localPosition = Vector3.zero; // Asegura que el objeto esté centrado en la caja
        Debug.Log($"Objeto {other.name} se colocó en la caja.");
    }

    // Detectar cuando el objeto sale de la caja
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && currentObject == other.gameObject)
        {
            Debug.Log($"Objeto {other.name} salió de la caja.");
            currentObject = null; // Limpiar el objeto cuando salga
        }
    }

    // Devolver el objeto a su posición original si no se puede quedar en la caja
    private void ReturnToOriginalPosition(GameObject obj)
    {
        var dragScript = obj.GetComponent<DragObject2D>();
        if (dragScript != null) dragScript.ReturnToOriginalPosition();
    }

    // Método para mover un objeto de una caja a otra
    public void MoverObjetoACaja(GameObject objeto, BoxZone2D nuevaCaja)
    {
        if (currentObject != null)
        {
            // Eliminar objeto de la caja actual
            currentObject.transform.SetParent(null);
            currentObject = null;
        }

        // Mover objeto a la nueva caja
        nuevaCaja.currentObject = objeto;
        objeto.transform.SetParent(nuevaCaja.transform);
        objeto.transform.localPosition = Vector3.zero;
    }
}
