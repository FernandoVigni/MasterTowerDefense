using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
        }
    }

    private void Start()
    {
        PlayMusic("MainMenu");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);
        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.clip = sound.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    /*cambiar a si el vol es = 0 
        if (musicSource.mute) 
        {
            MainMenu.Instance.musicButtonOn.SetActive(false);
            MainMenu.Instance.musicButtonOff.SetActive(true);
        }*/
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolumen(float volume)
    {
        musicSource.volume = volume;
        if (volume <= 0)
        {
            MainMenu.Instance.musicButtonOn.SetActive(false);
            MainMenu.Instance.musicButtonOff.SetActive(true);
        }
        else 
        {
            MainMenu.Instance.musicButtonOn.SetActive(true);
            MainMenu.Instance.musicButtonOff.SetActive(false);
        }
    }

    public void SFXVolumen(float volume)
    {
        sfxSource.volume = volume;
        if (volume <= 0)
        {
            MainMenu.Instance.sfxButtonOn.SetActive(false);
            MainMenu.Instance.sfxButtonOff.SetActive(true);
        }
        else 
        {
            MainMenu.Instance.sfxButtonOn.SetActive(true);
            MainMenu.Instance.sfxButtonOff.SetActive(false);
        }
    }

}
