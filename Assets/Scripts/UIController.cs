using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        // Load and apply saved volume settings
        float savedMusicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFXVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        _musicSlider.value = savedMusicVol;
        _sfxSlider.value = savedSFXVol;

        AudioManager.Instance.MusicVolume(savedMusicVol);
        AudioManager.Instance.SFXVolume(savedSFXVol);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        float volume = _musicSlider.value;
        AudioManager.Instance.MusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SFXVolume()
    {
        float volume = _sfxSlider.value;
        AudioManager.Instance.SFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
