using UnityEngine;

public class CajaManager : MonoBehaviour
{
    public GameObject[] cajasArriba;
    public GameObject[] cajasAbajo;

    private ActivarObjetos activarObjetos;

    private void Start()
    {
        // Buscar el script ActivarObjetos en la escena
        activarObjetos = FindObjectOfType<ActivarObjetos>();
        if (activarObjetos == null)
        {
            Debug.LogError("No se encontr� el script ActivarObjetos en la escena.");
        }

        // Activar objetos seg�n el contador
        ActivarObjetosSegunContador();
    }

    // Activar objetos (cajas) seg�n los contadores
    public void ActivarObjetosSegunContador()
    {
        // Activamos las cajas de arriba seg�n el contador de cajas arriba activas
        for (int i = 0; i < cajasArriba.Length; i++)
        {
            if (cajasArriba[i] != null)
            {
                cajasArriba[i].SetActive(i < ActivarObjetos.objetosArribaActivos); // Activa las cajas arriba
                Debug.Log($"Caja arriba {cajasArriba[i].name} {(i < ActivarObjetos.objetosArribaActivos ? "activada" : "desactivada")}");
            }
        }

        // Activamos las cajas de abajo seg�n el contador de cajas abajo activas
        for (int i = 0; i < cajasAbajo.Length; i++)
        {
            if (cajasAbajo[i] != null)
            {
                cajasAbajo[i].SetActive(i < ActivarObjetos.objetosAbajoActivos); // Activa las cajas abajo
                Debug.Log($"Caja abajo {cajasAbajo[i].name} {(i < ActivarObjetos.objetosAbajoActivos ? "activada" : "desactivada")}");
            }
        }
    }

    // Funci�n para desactivar todas las cajas de abajo y sus contenidos
    public void DesactivarCajasAbajo()
    {
        if (cajasAbajo.Length == 0)
        {
            Debug.LogWarning("No se encontraron cajas de abajo para desactivar.");
            return;
        }

        foreach (GameObject caja in cajasAbajo)
        {
            caja.SetActive(false); // Desactiva la caja
            // Eliminar personajes generados dentro de las cajas
            foreach (Transform child in caja.transform)
            {
                Destroy(child.gameObject); // Elimina los personajes dentro de las cajas
            }
            Debug.Log($"Caja de abajo {caja.name} desactivada y personajes eliminados.");
        }
    }
}
