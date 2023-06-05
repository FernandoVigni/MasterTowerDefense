using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.VisualScripting;

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
    [SerializeField] public GameObject victory;
    [SerializeField] public GameObject lose;
    [SerializeField] public GameObject loading;
    [SerializeField] public ProphecyScreen prophecyScreen;
    [SerializeField] public GameObject musicButtonOn;
    [SerializeField] public GameObject musicButtonOff;
    [SerializeField] public GameObject sfxButtonOn;
    [SerializeField] public GameObject sfxButtonOff;
    [SerializeField] public GameObject game;

    private AudioManager audioManager;
    public static MainMenu Instance;

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();

        if (Instance == null)
        {
            Instance = this;    
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        
    }

    public void SetRenderCamera() 
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
    }

    public async void Play()
    {
        audioManager.PlaySFX("Button");
        Time.timeScale = 0f;
        loading.SetActive(true);
        mainMenu.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        await Task.Delay(2000);
        prophecyScreen.gameObject.SetActive(true);
        loading.SetActive(false);
        prophecyScreen.StartProphecyScene();

        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);

        Time.timeScale = 1f;
    }

    public void Pause()
    {
        audioManager.PlaySFX("Button");
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        optionsInGameMenu.SetActive(true);
        menuOptions.SetActive(true);
        //Bajar el Volumen
    }

    public void Resume()
    {
        audioManager.PlaySFX("Button");
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
        Time.timeScale = 1f;
        //subir el volumen que baje
    }

    public void Contact()
    {
        audioManager.PlaySFX("Button");
        contact.SetActive(true);
    }

    public void Home()
    {
        audioManager.PlaySFX("Button");
        contact.SetActive(false);
        confirmation.SetActive(false);
        volumen.SetActive(false);
    }

    public void CheckReturnToMainMenu() 
    {
        audioManager.PlaySFX("Button");
        confirmation.SetActive(true);
    }

    public void CloseConfirmation() 
    {
        audioManager.PlaySFX("Button");
        confirmation.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        audioManager.PlaySFX("Button");
        //    volumen.SetActive(false);
        //  contact.SetActive(false);
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        menuOptions.SetActive(false);
        confirmation.SetActive(false);
        mainMenu.SetActive(true);
        victory.SetActive(false);
        lose.SetActive(false);
        //subir el volumen que baje
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        audioManager.PlayMusic("MainMenu");
    }

    public void Victory() 
    {
        victory.SetActive(true);
    }

    public void Lose() 
    {
        lose.SetActive(true);
    }

    public void Volumen() 
    {
        audioManager.PlaySFX("Button");
        volumen.SetActive(true);
    }

    public void Exit()
    {
        audioManager.PlaySFX("Button");
        Debug.Log("Exit...");
        Application.Quit();
    }
}
