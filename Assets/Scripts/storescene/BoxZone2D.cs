using UnityEngine;

public class BoxZone2D : MonoBehaviour
{
    public GameObject currentObject = null;
    private CajaManager cajaManager;

    private void Start()
    {
        cajaManager = FindObjectOfType<CajaManager>();
    }

    private void OnMouseDown() // Detectar clic en la caja
    {
        if (currentObject != null && cajaManager != null)
        {
            // Verificar que haya una caja arriba disponible
            for (int i = 0; i < cajaManager.cajasArriba.Length; i++)
            {
                if (cajaManager.cajasArriba[i].activeSelf && cajaManager.cajasArriba[i].transform.childCount == 0)
                {
                    // Mover el personaje a una caja arriba vacía
                    BoxZone2D cajaArriba = cajaManager.cajasArriba[i].GetComponent<BoxZone2D>();
                    cajaArriba.MoverObjetoACaja(currentObject, cajaArriba);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Detectar cuando un objeto entra en la caja
    {
        if (!other.CompareTag("Personaje")) return; // Solo procesamos si es un "Personaje"

        if (currentObject != null) // Si la caja ya tiene un objeto, no aceptar otro
        {
            ReturnToOriginalPosition(other.gameObject);
            return;
        }

        currentObject = other.gameObject; // Si la caja está vacía, poner el objeto dentro

        other.transform.SetParent(transform);
        other.transform.localPosition = Vector3.zero;
        Debug.Log($"Objeto {other.name} se colocó en la caja.");
    }

    private void OnTriggerExit2D(Collider2D other) // Detectar cuando el objeto sale de la caja
    {
        if (other.CompareTag("Personaje") && currentObject == other.gameObject)
        {
            currentObject = null; // Limpiar el objeto cuando salga
        }
    }

    private void ReturnToOriginalPosition(GameObject obj) // Devolver el objeto a su posición original si no se puede quedar en la caja
    {
             
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
