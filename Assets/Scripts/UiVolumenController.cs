using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiVolumenController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private TextMeshProUGUI volumenMusicTextUI, volumenSFXTextUI;

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
    }
    public void SFXVolumen()
    {
        AudioManager.Instance.SFXVolumen(sfxSlider.value);
    }

}
