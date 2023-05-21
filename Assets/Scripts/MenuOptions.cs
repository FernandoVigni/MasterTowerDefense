using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject menuPause;
    public void Pause()
    {
        Time.timeScale = 0f;
        optionsButton.SetActive(false);
        menuPause.SetActive(true);
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
        optionsButton.SetActive(true);
        menuPause.SetActive(false);
    }

    public void ReturnToMainMenu() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
