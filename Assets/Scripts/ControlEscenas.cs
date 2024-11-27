using UnityEngine;

public class ControlEscenas : MonoBehaviour
{
    public CajaManager cajaManager;
    public ActivarObjetos activarObjetos;

    private void Start()
    {
        // Buscar el CajaManager en la escena
        cajaManager = FindObjectOfType<CajaManager>();

        // Comprobar si CajaManager fue encontrado
        if (cajaManager == null)
        {
            Debug.LogWarning("CajaManager no encontrado en la escena.");
        }

        // Buscar el ActivarObjetos en la escena
        activarObjetos = FindObjectOfType<ActivarObjetos>();

        // Comprobar si ActivarObjetos fue encontrado
        if (activarObjetos == null)
        {
            Debug.LogWarning("ActivarObjetos no encontrado en la escena.");
        }
    }

    // Incrementa el contador de objetos (si está presente ActivarObjetos)
    public void IncrementarContador()
    {
        if (activarObjetos != null)
        {
            activarObjetos.IncrementarContador(); // Incrementar el contador de cajas
        }
        else
        {
            Debug.LogWarning("ActivarObjetos es nulo. No se puede incrementar el contador.");
        }
    }

    // Función para desactivar las cajas de abajo y sus contenidos
    public void DesactivarCajasAbajo()
    {
        if (cajaManager != null)
        {
            cajaManager.DesactivarCajasAbajo(); // Desactiva las cajas de abajo
        }
        else
        {
            Debug.LogWarning("CajaManager no está asignado. No se puede desactivar las cajas de abajo.");
        }
    }
}
