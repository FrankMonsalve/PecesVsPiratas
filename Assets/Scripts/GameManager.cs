using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<GameObject> objetosArrastrados = new List<GameObject>();  // Lista para almacenar los objetos arrastrados

    // Asegurarse de que el GameManager persista entre escenas
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // El GameManager no se destruirá al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Agregar un objeto arrastrado a la lista
    public void AgregarObjetoArrastrado(GameObject objeto)
    {
        if (!objetosArrastrados.Contains(objeto))
        {
            objetosArrastrados.Add(objeto);
        }
    }

    // Obtener los objetos arrastrados
    public List<GameObject> ObtenerObjetosArrastrados()
    {
        return objetosArrastrados;
    }

    // Limpiar la lista de objetos cuando se carga una nueva escena
    public void LimpiarObjetos()
    {
        objetosArrastrados.Clear();
    }
}
