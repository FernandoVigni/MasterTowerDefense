using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject buttonOptions;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject goldStatus;
  public void Pause()
    {
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        menuPause.SetActive(true);
        goldStatus.SetActive(false);
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
        buttonOptions.SetActive(true);
        menuPause.SetActive(false);
        goldStatus.SetActive(true);
    }

    public void ReturnToMainMenu() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
