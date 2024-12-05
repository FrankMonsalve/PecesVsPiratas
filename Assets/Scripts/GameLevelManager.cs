using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esto para usar UI

public class GameLevelManager : MonoBehaviour
{
    public GameObject VolumeSlider; // Asigna tu Slider en el Inspector
    public float yourVolume = 0.5f; // Valor inicial del volumen

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Debug.Log("Se inició la escena");

        if (VolumeSlider != null)
        {
            Slider sliderComponent = VolumeSlider.GetComponent<Slider>();
            if (sliderComponent != null)
            {
                sliderComponent.value = yourVolume;
            }
            else
            {
                Debug.LogError("No se encontró el componente Slider en el objeto VolumeSlider.");
            }
        }
        else
        {
            Debug.LogError("VolumeSlider no está asignado en el Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

