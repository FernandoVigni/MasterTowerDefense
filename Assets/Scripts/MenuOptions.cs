using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject buttonOptions;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject goldStatus;
    [SerializeField] private GameObject contact;
  public void Pause()
    {
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
    }

    public void Contact()
    {
        contact.SetActive(true);
    }

    public void Home() 
    {
        contact.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        menuOptions.SetActive(false);
        contact.SetActive(false);
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
