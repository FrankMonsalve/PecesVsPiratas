using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string DataToPass; // Dato compartido entre escenas

    public void LoadScene(string sceneName, string data = "")
    {
        DataToPass = data; // Guarda el dato para la siguiente escena
        SceneManager.LoadScene(sceneName);
    }

    public static string GetPassedData()
    {
        string data = DataToPass;
        DataToPass = ""; // Limpia después de usarlo
        return data;
    }
}