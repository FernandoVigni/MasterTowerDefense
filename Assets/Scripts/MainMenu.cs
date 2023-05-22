using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject contact;
    [SerializeField] private GameObject menuOptions;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Contact()
    {
        contact.SetActive(true);
    }

    public void Options()
    {

    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }

    //-------------------------

    public void Home()
    {
        contact.SetActive(false);
    }

}
