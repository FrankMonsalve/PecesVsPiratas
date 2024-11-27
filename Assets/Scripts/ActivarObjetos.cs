using UnityEngine;

public class ActivarObjetos : MonoBehaviour
{
    public static int objetosArribaActivos = 1;
    public static int objetosAbajoActivos = 3;

    public GameObject generadorPersonajes;
    private GenerarPersonajes generarPersonajesScript;

    private GameObject cajaArriba;
    private bool cajaArribaOcupada = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Persistir entre escenas
    }

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
            Debug.LogError("No se encontró el script GenerarPersonajes.");
            return;
        }

        ActivarObjetosSegunContador();
        cajaArriba = GameObject.FindGameObjectWithTag("boxUp");
        if (cajaArriba == null)
            Debug.LogError("No se encontró la caja con el tag 'boxUp'.");
    }

    public void IncrementarContador()
    {
        if (objetosArribaActivos < 6) objetosArribaActivos++;
        if (objetosAbajoActivos < 8) objetosAbajoActivos++;
        Debug.Log($"Contadores incrementados: Arriba {objetosArribaActivos}, Abajo {objetosAbajoActivos}");
    }

    public void ActivarObjetosSegunContador()
    {
        ActivarObjetosPorTag("boxUp", objetosArribaActivos);
        ActivarObjetosPorTag("boxDown", objetosAbajoActivos);

        if (generarPersonajesScript != null)
            generarPersonajesScript.GenerarPersonajesEnCajas();
    }

    private void ActivarObjetosPorTag(string tag, int limite)
    {
        var objetos = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < objetos.Length; i++)
            objetos[i].SetActive(i < limite);
    }

    public void LiberarCajaArriba()
    {
        cajaArribaOcupada = false;
        Debug.Log("Caja arriba liberada.");
    }

    // Método para manejar la colocación del objeto arrastrado
    public void ColocarEnCaja(GameObject objeto)
    {
        if (cajaArriba != null && !cajaArribaOcupada)
        {
            objeto.transform.position = cajaArriba.transform.position; // Colocamos el objeto en la caja
            objeto.transform.SetParent(cajaArriba.transform); // Lo convertimos en hijo de la caja
            cajaArribaOcupada = true; // Marcamos que la caja está ocupada
            Debug.Log("Objeto colocado dentro de la caja.");
        }
    }
}