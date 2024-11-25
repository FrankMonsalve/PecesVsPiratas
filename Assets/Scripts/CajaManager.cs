using UnityEngine;
using UnityEngine.SceneManagement;

public class CajaManager : MonoBehaviour
{
    public GameObject[] cajasArriba;
    public GameObject[] cajasAbajo;

    void Awake() => DontDestroyOnLoad(gameObject);

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Escena cargada: {scene.name}");
        ActivarObjetosSegunContador();
    }

    // Activar objetos según el contador de objetos activos
    public void ActivarObjetosSegunContador()
    {
        for (int i = 0; i < cajasArriba.Length; i++)
            if (cajasArriba[i] != null)
                cajasArriba[i].SetActive(i < ActivarObjetos.objetosArribaActivos);

        for (int i = 0; i < cajasAbajo.Length; i++)
            if (cajasAbajo[i] != null)
                cajasAbajo[i].SetActive(i < ActivarObjetos.objetosAbajoActivos);
    }

    // Método para colocar el objeto dentro de la caja
    public void ColocarObjetoEnCaja(GameObject objeto, Transform caja)
    {
        CajaColocacion.ColocarObjetoEnCaja(objeto, caja);  // Usar el método centralizado
    }
}
