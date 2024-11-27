using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   [SerializeField] GameObject pauseMenuPanel;
   public void Pause()
   {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
   }

   public void Home()
   {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
   }

   public void Resume()
   {
     pauseMenuPanel.SetActive(false);
     Time.timeScale = 1;
   }

   public void Restart()
   {
        
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
