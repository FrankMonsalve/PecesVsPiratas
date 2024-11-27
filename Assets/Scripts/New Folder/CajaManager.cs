using UnityEngine;
using UnityEngine.UI;

public class CajaManager : MonoBehaviour
{
    [Header("Referencias a Cajas")]
    public GameObject[] cajasArriba; // Referencia a las cajas de arriba
    public GameObject[] cajasAbajo; // Referencia a las cajas de abajo

    [Header("Contadores")]
    public static int objetosArribaActivos = 1; // Cantidad inicial de cajas activas arriba
    public static int objetosAbajoActivos = 3; // Cantidad inicial de cajas activas abajo

    [Header("Generador de Personajes")]
    public GenerarPersonajes generarPersonajesScript; // Referencia al script de generación de personajes

    [Header("UI Botones")]
    [SerializeField] private Button botonIncrementar; // Botón para incrementar contadores
    [SerializeField] private Button botonDesactivar;  // Botón para desactivar cajas abajo

    private void Start()
    {
        if (generarPersonajesScript == null)
        {
            Debug.LogError("Generador de personajes no asignado.");
            return;
        }

        if (botonIncrementar != null)
        {
            botonIncrementar.onClick.RemoveAllListeners();
            botonIncrementar.onClick.AddListener(IncrementarContadores);
        }

        if (botonDesactivar != null)
        {
            botonDesactivar.onClick.RemoveAllListeners();
            botonDesactivar.onClick.AddListener(DesactivarCajasAbajo);
        }

        // Activar cajas según el estado inicial
        ActivarCajasSegunContador();
    }

    public void ActivarCajasSegunContador()
    {
        Debug.Log("Activando cajas según contadores...");

        // Activar cajas arriba
        ActivarCajas(cajasArriba, objetosArribaActivos);

        // Activar cajas abajo
        ActivarCajas(cajasAbajo, objetosAbajoActivos);

        // Generar personajes SOLO después de actualizar las cajas
        generarPersonajesScript.GenerarPersonajesEnCajas();
    }

    public void IncrementarContadores()
    {
        if (objetosArribaActivos < cajasArriba.Length) objetosArribaActivos++;
        if (objetosAbajoActivos < cajasAbajo.Length) objetosAbajoActivos++;

        Debug.Log($"Contadores incrementados: Arriba {objetosArribaActivos}, Abajo {objetosAbajoActivos}");
        ActivarCajasSegunContador();
    }

    public void DesactivarCajasAbajo()
    {
        foreach (GameObject caja in cajasAbajo)
        {
            if (caja != null)
            {
                caja.SetActive(false);

                // Eliminar contenido de la caja
                while (caja.transform.childCount > 0)
                {
                    DestroyImmediate(caja.transform.GetChild(0).gameObject);
                }

                Debug.Log($"Caja {caja.name} desactivada y contenido eliminado.");
            }
        }
    }

    private void ActivarCajas(GameObject[] cajas, int limiteActivos)
    {
        for (int i = 0; i < cajas.Length; i++)
        {
            if (cajas[i] != null)
            {
                cajas[i].SetActive(i < limiteActivos);
                Debug.Log($"Caja {cajas[i].name} {(i < limiteActivos ? "activada" : "desactivada")}.");
            }
        }
    }
}
