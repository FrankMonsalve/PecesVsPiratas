using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
        Time.timeScale = 1;
    }

}
