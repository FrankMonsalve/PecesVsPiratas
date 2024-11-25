using UnityEngine;

public class CajaColocacion : MonoBehaviour
{
    // M�todo centralizado para colocar el objeto en una caja
    public static void ColocarObjetoEnCaja(GameObject objeto, Transform caja)
    {
        Caja cajaScript = caja.GetComponent<Caja>(); // Suponiendo que la caja tiene un script llamado "Caja"
        if (caja != null && !cajaScript.IsCajaOcupada())
        {
            // Marca la caja como ocupada y mueve el objeto
            cajaScript.MarcarCajaOcupada();
            objeto.transform.position = caja.position;
            Debug.Log("Objeto colocado correctamente en la caja.");
        }
        else
        {
            // Si no se puede colocar, se puede hacer otra acci�n, como devolverlo a su posici�n inicial
            Debug.Log("No se puede colocar el objeto en la caja.");
        }
    }

    // M�todo para liberar la caja
    public static void LiberarCaja(Transform caja)
    {
        if (caja != null)
        {
            caja.GetComponent<Caja>().MarcarCajaLibre(); // Libera la caja
            Debug.Log("Caja liberada.");
        }
    }
}
