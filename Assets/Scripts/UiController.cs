using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    //public Slider musicSlider, sfxSlider;

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
      //  AudioManager.Instance.MusicVolumen(musicSlider.value);
    }
    public void SFXVolumen()
    {
      //  AudioManager.Instance.SFXVolumen(sfxSlider.value);
    }

}
