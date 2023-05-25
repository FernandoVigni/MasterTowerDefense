using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject contact;
    [SerializeField] private GameObject volumen;
    [SerializeField] private GameObject goldStatus;
    [SerializeField] private GameObject buttonOptions;
    [SerializeField] public GameObject optionsInGameMenu;
    [SerializeField] public GameObject menuOptions;
    [SerializeField] public GameObject confirmation;

    public static MainMenu Instance;

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

    public void Play()
    {


            // Here Load Screen

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        mainMenu.SetActive(false);
        optionsInGameMenu.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        optionsInGameMenu.SetActive(true);
        menuOptions.SetActive(true);
    }

    public void Resume()
    {
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Contact()
    {
        contact.SetActive(true);
    }

    public void Home()
    {
        contact.SetActive(false);
    }

    public void CheckReturnToMainMenu() 
    {
        confirmation.SetActive(true);
    }

    public void CloseConfirmation() 
    {
        confirmation.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
    //    volumen.SetActive(false);
      //  contact.SetActive(false);
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        menuOptions.SetActive(false);
        confirmation.SetActive(false);
        mainMenu.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Volumen() 
    {
        
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
}
