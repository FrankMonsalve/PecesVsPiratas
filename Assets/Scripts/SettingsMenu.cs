using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    [Header(".............Audio Source...........")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header(".............Audio Clip...........")]
    public AudioClip music;

    public void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }


    public void SetVolume (float volume)
    {
        //Time.timeScale = 1;
        audioMixer.SetFloat("volume", volume);
    }

}
