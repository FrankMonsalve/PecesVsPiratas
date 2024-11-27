using UnityEngine;
using UnityEngine.UI;

public class ActivarObjetos : MonoBehaviour
{
    public static int objetosArribaActivos = 1;
    public static int objetosAbajoActivos = 3;

    public GameObject generadorPersonajes;
    private GenerarPersonajes generarPersonajesScript;

    public GameObject[] cajasArriba;
    public GameObject[] cajasAbajo;

    public Button botonIncrementar; // Bot�n para incrementar
    public Button botonDesactivar;  // Bot�n para desactivar las cajas de abajo

    private void Start()
    {
        if (generadorPersonajes == null)
        {
            Debug.LogError("Generador de personajes no asignado.");
            return;
        }

        generarPersonajesScript = generadorPersonajes.GetComponent<GenerarPersonajes>();
        if (generarPersonajesScript == null)
        {
            Debug.LogError("No se encontr� el script GenerarPersonajes.");
            return;
        }

        // Asociamos las funciones a los botones
        if (botonIncrementar != null)
            botonIncrementar.onClick.AddListener(IncrementarContador);

        if (botonDesactivar != null)
            botonDesactivar.onClick.AddListener(DesactivarCajasAbajo);

        // Llamamos a ActivarObjetosSegunContador para inicializar las cajas activas
        ActivarObjetosSegunContador();
    }
        

    // Desactiva las cajas de abajo y sus personajes generados
    public void DesactivarCajasAbajo()
    {
        foreach (GameObject caja in cajasAbajo)
        {
            caja.SetActive(false); // Desactiva la caja
            // Eliminar personajes dentro de las cajas (si hay)
            foreach (Transform child in caja.transform)
            {
                Destroy(child.gameObject);
            }
        }
        Debug.Log("Cajas de abajo desactivadas.");
    }


    // Funci�n para activar los objetos por tag y el l�mite
    public void ActivarObjetosPorTag(string tag, int limite)
    {
        // Encontrar todos los objetos con el tag especificado
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log($"Activando objetos con tag: {tag}, L�mite: {limite}, Cajas encontradas: {objetos.Length}");

        // Activar los objetos seg�n el l�mite
        for (int i = 0; i < objetos.Length; i++)
        {
            if (i < limite)
            {
                objetos[i].SetActive(true);  // Activar el objeto si est� dentro del l�mite
                Debug.Log($"Caja {objetos[i].name} activada.");
            }
            else
            {
                objetos[i].SetActive(false);  // Desactivar el objeto si supera el l�mite
                Debug.Log($"Caja {objetos[i].name} desactivada.");
            }
        }
    }


    // Funci�n para reactivar las cajas de abajo
    private void ReactivarCajasAbajo()
    {
        // Siempre intentar reactivar las cajas de abajo seg�n el contador
        for (int i = 0; i < cajasAbajo.Length; i++)
        {
            if (cajasAbajo[i] != null)
            {
                // Activamos las cajas abajo seg�n el contador
                cajasAbajo[i].SetActive(i < objetosAbajoActivos);
            }
        }

        // Opcional: Si no hay cajas activas, puede ser �til mostrar un mensaje
        if (objetosAbajoActivos == 0)
        {
            Debug.LogWarning("No hay cajas activas en abajo.");
        }
    }

    public void IncrementarContador()
    {
        // Incrementar el contador de cajas arriba y abajo
        if (objetosArribaActivos < 6) objetosArribaActivos++; // Limitar el m�ximo de cajas arriba
        if (objetosAbajoActivos < 8) objetosAbajoActivos++;   // Limitar el m�ximo de cajas abajo

        Debug.Log($"Contadores incrementados: Arriba {objetosArribaActivos}, Abajo {objetosAbajoActivos}");

        // Llamamos a ActivarObjetosSegunContador para activar las cajas y generar los personajes
        ActivarObjetosSegunContador();
    }

    public void ActivarObjetosSegunContador()
    {
        Debug.Log("Activando objetos seg�n los contadores...");

        // Activar cajas arriba y abajo
        ActivarCajas(); // Activar las cajas antes de operar sobre ellas

        // Ahora que las cajas est�n activas, proceder con la activaci�n
        ActivarObjetosPorTag("boxUp", objetosArribaActivos);  // Activamos las cajas arriba
        ActivarObjetosPorTag("boxDown", objetosAbajoActivos); // Activamos las cajas abajo

        // Generar los personajes en las cajas activas de abajo
        if (generarPersonajesScript != null)
        {
            generarPersonajesScript.GenerarPersonajesEnCajas();
        }
    }

    // Activar todas las cajas desactivadas
    private void ActivarCajas()
    {
        // Activamos las cajas de arriba
        foreach (GameObject caja in cajasArriba)
        {
            if (caja != null)
            {
                caja.SetActive(true); // Activamos la caja
            }
        }

        // Activamos las cajas de abajo
        foreach (GameObject caja in cajasAbajo)
        {
            if (caja != null)
            {
                caja.SetActive(true); // Activamos la caja
            }
        }

        Debug.Log("Cajas activadas.");
    }

}
