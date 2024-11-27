using UnityEngine;

public class CajaRestrictiva : MonoBehaviour
{
    private Transform cajaDetectada;  // Caja detectada
    private bool esCajaV�lida = true; // Bandera para determinar si la caja es v�lida para colocar un objeto

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

    // Coloca el objeto si la caja es v�lida
    public void ColocarObjeto(GameObject objeto)
    {
        if (cajaDetectada != null)
        {
            // Verifica si la caja es v�lida (puedes agregar m�s condiciones aqu�)
            if (esCajaV�lida)
            {
                CajaColocacion.ColocarObjetoEnCaja(objeto, cajaDetectada);  // Llama al m�todo centralizado para colocar el objeto
            }
            else
            {
                Debug.Log("Caja no v�lida para colocar el objeto.");
            }
        }
    }

    // M�todo para establecer las restricciones de la caja
    public void EstablecerRestricciones(bool esValida)
    {
        esCajaV�lida = esValida;
    }
}

