using UnityEngine;

public class CajaRestrictiva : MonoBehaviour
{
    private Transform cajaDetectada;  // Caja detectada
    private bool esCajaVálida = true; // Bandera para determinar si la caja es válida para colocar un objeto

    // Se activa cuando el objeto entra en la zona de la caja
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("boxUp"))
        {
            cajaDetectada = collision.transform;
        }
    }

    // Se activa cuando el objeto sale de la zona de la caja
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("boxUp") && cajaDetectada == collision.transform)
        {
            cajaDetectada = null;
        }
    }

    // Coloca el objeto si la caja es válida
    public void ColocarObjeto(GameObject objeto)
    {
        if (cajaDetectada != null)
        {
            // Verifica si la caja es válida (puedes agregar más condiciones aquí)
            if (esCajaVálida)
            {
                CajaColocacion.ColocarObjetoEnCaja(objeto, cajaDetectada);  // Llama al método centralizado para colocar el objeto
            }
            else
            {
                Debug.Log("Caja no válida para colocar el objeto.");
            }
        }
    }

    // Método para establecer las restricciones de la caja
    public void EstablecerRestricciones(bool esValida)
    {
        esCajaVálida = esValida;
    }
}

