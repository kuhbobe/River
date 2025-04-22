using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {



        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            
        }
        
        Load();

        // Add an event listener to the volumeSlider
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float value)
    {
        Debug.Log("Volume changed to: " + value);
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = savedVolume; // Update the slider's value
        AudioListener.volume = savedVolume; // Update the audio volume
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
