using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esto para usar UI

public class GameLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _StartingSceneTransition;
    [SerializeField] private GameObject _EndingSceneTransition;

    void Start()
    {
        _StartingSceneTransition.SetActive(true);
        Time.timeScale = 1;
        Debug.Log("Se inició la escena");
        
        

    }

    private void DesibleStartingTransition()
    {
        _StartingSceneTransition.SetActive(false);
    }

}

