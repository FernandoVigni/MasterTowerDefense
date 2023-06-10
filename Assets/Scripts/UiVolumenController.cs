using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiVolumenController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private TextMeshProUGUI volumenMusicTextUI, volumenSFXTextUI;

    private void Start()
    {
        AudioManager.Instance.MusicVolumen(0.2f);
        AudioManager.Instance.SFXVolumen(0.2f);
        musicSlider.value = 0.8f;
        sfxSlider.value = 0.8f;
    }

    public void ToggleMusic() 
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX() 
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolumen() 
    {
        AudioManager.Instance.MusicVolumen(musicSlider.value);
        int musicVolumen = Mathf.RoundToInt(AudioManager.Instance.musicSource.volume * 100);
        volumenMusicTextUI.text = musicVolumen.ToString();
    }
    public void SFXVolumen()
    {
        AudioManager.Instance.SFXVolumen(sfxSlider.value);
        int sfxVolumen = Mathf.RoundToInt(AudioManager.Instance.sfxSource.volume * 100);
        volumenSFXTextUI.text = sfxVolumen.ToString();
    }
}
