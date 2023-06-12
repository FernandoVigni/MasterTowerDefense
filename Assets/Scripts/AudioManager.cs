using System;
using UnityEngine;
using UnityEngine.Rendering;

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
        if (musicSource.volume <= 0)
        {
            MainMenu.Instance.musicButtonOn.SetActive(false);
            MainMenu.Instance.musicButtonOff.SetActive(true);
        }
        else
        {
            if (musicSource.mute)
            {
                musicSource.mute = false;
                MainMenu.Instance.musicButtonOn.SetActive(true);
                MainMenu.Instance.musicButtonOff.SetActive(false);

            }
            else 
            {
                musicSource.mute = true;
                MainMenu.Instance.musicButtonOn.SetActive(false);
                MainMenu.Instance.musicButtonOff.SetActive(true);
            }
        }
    }

    public void MusicVolumen(float volume)
    {
        MainMenu.Instance.musicButtonOff.SetActive(true);
        musicSource.mute = false;
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

    public void ToggleSFX()
    {
        if (sfxSource.volume <= 0)
        {
            MainMenu.Instance.sfxButtonOn.SetActive(false);
            MainMenu.Instance.sfxButtonOff.SetActive(true);
        }
        else
        {
            if (sfxSource.mute)
            {
                sfxSource.mute = false;
                MainMenu.Instance.sfxButtonOn.SetActive(true);
                MainMenu.Instance.sfxButtonOff.SetActive(false);
            }
            else
            {
                sfxSource.mute = true;
                MainMenu.Instance.sfxButtonOn.SetActive(false);
                MainMenu.Instance.sfxButtonOff.SetActive(true);
            }
        }
    }

    public void SFXVolumen(float volume)
    {
        MainMenu.Instance.sfxButtonOff.SetActive(true);
        sfxSource.mute = false;
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
