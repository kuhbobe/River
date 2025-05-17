using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
   
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public bool IsThemePlaying(string name)
{
    return musicSource.isPlaying && musicSource.clip != null && musicSource.clip.name == name;
}


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

     
        float savedMusicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFXVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = savedMusicVol;
        sfxSource.volume = savedSFXVol;
    }


    private void Start()
    {
       PlayMusic("Lofi");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else { 
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x=> x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else { 
            sfxSource.PlayOneShot(s.clip);
        }
    }

    private bool isThemePlaying = false;

public void ToggleTheme(string name)
{
    if (musicSource.isPlaying && musicSource.clip != null)
    {
        if (musicSource.clip.name == name)
        {
            // If the same theme is playing, stop it
            musicSource.Stop();
            return;
        }
    }

    // Play or override with the new theme
    Sound s = Array.Find(musicSounds, x => x.name == name);
    if (s != null)
    {
        musicSource.clip = s.clip;
        musicSource.Play();
    }
    else
    {
        Debug.LogWarning("Theme not found: " + name);
    }
}

public void ToggleMusic()
{
    musicSource.mute = !musicSource.mute;
}
public void ToggleSFX()
{
    sfxSource.mute = !sfxSource.mute;
}

public void MusicVolume(float volume)
{
    musicSource.volume = volume;
}

public void SFXVolume(float volume)
{
    sfxSource.volume = volume;
}

}
