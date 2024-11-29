using System.Collections.Generic;
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

    public GameObject spawnPoint;  // Referencia al GameObject SpawnPoint en la escena
    public float offsetX = 2f;

    // Variable para guardar las referencias a los clones generados
    private List<GameObject> clonesGenerados = new List<GameObject>();

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
            botonDesactivar.onClick.AddListener(DesactivarCajasAbajoYGenerarSpawn);
        }

        // Activar cajas según el estado inicial
        ActivarCajasSegunContador();
    }

    public void ActivarCajasSegunContador()
    {
        Debug.Log("Activando cajas según contadores...");

        // Eliminar los clones anteriores antes de generar nuevos
        EliminarClonesGenerados();

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

    public void DesactivarCajasAbajoYGenerarSpawn()
    {
        Vector3 spawnPosition = spawnPoint.transform.position; // Obtener la posición inicial del spawn

        // Eliminar los clones generados previamente
        EliminarClonesGenerados();

        // Iterar por todas las cajas de arriba
        foreach (GameObject caja in cajasArriba)
        {
            if (caja != null)
            {
                // Verificar si hay un prefab dentro de la caja
                if (caja.transform.childCount > 0)
                {
                    // Obtener el prefab dentro de la caja
                    GameObject prefabOriginal = caja.transform.GetChild(0).gameObject;

                    // Crear una copia del prefab
                    GameObject clon = Instantiate(prefabOriginal, spawnPosition, Quaternion.identity);

                    // Añadir la copia a la lista de clones generados
                    clonesGenerados.Add(clon);

                    // Modificar la posición de la copia en el eje X
                    spawnPosition.y += offsetX;  // Alinea cada prefab bajo el anterior

                    // Desactivar la caja visualmente (sin destruir el prefab dentro)
                    caja.SetActive(false);
                }
            }
        }

        // Desactivar las cajas de abajo
        DesactivarCajasAbajo();
    }

    private void EliminarClonesGenerados()
    {
        // Eliminar todos los clones previamente generados
        foreach (GameObject clon in clonesGenerados)
        {
            Destroy(clon);
        }

        // Limpiar la lista de clones generados
        clonesGenerados.Clear();

        Debug.Log("Clones generados eliminados.");
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
