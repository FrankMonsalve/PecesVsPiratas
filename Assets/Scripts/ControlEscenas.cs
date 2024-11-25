using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void IrANuevaEscena()
    {
        // Incrementar contadores antes de cambiar de escena
        if (cajaManager != null && activarObjetos != null)
        {
            activarObjetos.IncrementarContador();
        }
        else
        {
            if (cajaManager == null)
                Debug.LogWarning("CajaManager es nulo. No se puede incrementar el contador.");

            if (activarObjetos == null)
                Debug.LogWarning("ActivarObjetos es nulo. No se puede incrementar el contador.");
        }

        // Cambiar a la siguiente escena
        SceneManager.LoadScene(1); // Reemplaza con el índice o nombre de la escena destino
    }

    public void RegresarAEscenaPrincipal()
    {
        // Cambiar a la escena principal
        SceneManager.LoadScene(0); // Reemplaza con el índice o nombre de la escena principal
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Activar objetos en la escena cuando se cargue
        if (cajaManager != null)
        {
            cajaManager.ActivarObjetosSegunContador();
        }
        else
        {
            Debug.LogWarning("CajaManager no está asignado. No se puede activar objetos.");
        }
    }

    private void OnEnable()
    {
        // Suscribir al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Cancelar suscripción al evento de carga de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}