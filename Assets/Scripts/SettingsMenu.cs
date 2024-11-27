using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        Time.timeScale = 1;
        audioMixer.SetFloat("volume", volume);
    }
}
