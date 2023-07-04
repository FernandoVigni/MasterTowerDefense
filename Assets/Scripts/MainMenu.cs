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
    [SerializeField] public GameObject saprksPlay;
    [SerializeField] public GameObject saprksLetsGoButton;
    [SerializeField] public GameObject shoot;
    [SerializeField] public GameObject randomHability;
    [SerializeField] public GameObject magicCircles;

    private EnemyManager enemyManager;
    private FireBallManager fireBallManager;
    public static MainMenu Instance;

    private void Awake()
    {
    
        enemyManager = FindAnyObjectByType<EnemyManager>();
        fireBallManager = FindAnyObjectByType<FireBallManager>();

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
        //shoot.SetActive(false);
        //AudioManager.Instance.PlaySFX("Button");
        //PhaseManager.instance.SetCurrentPhase0();
        //Time.timeScale = 0f;
        loading.SetActive(true);
        magicCircles.SetActive(true);
        TurnOffSparksPlayButton();
        mainMenu.SetActive(false);
        await Task.Delay(5500);
        magicCircles.SetActive(false);
        loading.SetActive(false);
        prophecyScreen.gameObject.SetActive(true);
        prophecyScreen.StartProphecyScene();
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX("Button");
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
        //shoot.SetActive(false);
        optionsInGameMenu.SetActive(true);
        menuOptions.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySFX("Button");
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        goldStatus.SetActive(true);
        //shoot.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Contact()
    {
        TurnOffSparksPlayButton();
        AudioManager.Instance.PlaySFX("Button");
        contact.SetActive(true);
    }

    public void Home()
    {
        AudioManager.Instance.PlaySFX("Button");
        contact.SetActive(false);
        confirmation.SetActive(false);
        volumen.SetActive(false);
        TurnOnSparksPlayButton();
    }

    public void CheckReturnToMainMenu() 
    {
        AudioManager.Instance.PlaySFX("Button");
        confirmation.SetActive(true);
    }

    public void CloseConfirmation() 
    {
        AudioManager.Instance.PlaySFX("Button");
        confirmation.SetActive(false);
    }

    public void CleanUiInGame() 
    {
        buttonOptions.SetActive(false);
        goldStatus.SetActive(false);
    }

    private void CloseMenuesUi() 
    {
        menuOptions.SetActive(false);
        confirmation.SetActive(false);
    }

    private void TurnOffScenario() 
    {
        game.SetActive(false);
    }

    private void TurnOnMainMenu() 
    {
        mainMenu.SetActive(true);
        TurnOnSparksPlayButton();
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlaySFX("Button");
        //fireBallManager.RemoveAllFireballs();
        enemyManager.RemoveAllInCollider();
        enemyManager.RemoveAllInStage();
        EnterInMainMenu();
    }

    public void EnterInMainMenu()
    {
        Time.timeScale = 1f;
        CleanUiInGame();
        CloseMenuesUi();
        TurnOffScenario();
        TurnOnMainMenu();
        AudioManager.Instance.PlayMusic("MainMenu");
        TurnOnSparksPlayButton();
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
        TurnOffSparksPlayButton();
        AudioManager.Instance.PlaySFX("Button");
        volumen.SetActive(true);
    }

    public void TurnOffSparksPlayButton() 
    {
        saprksPlay.SetActive(false);
    }

    public async Task TurnOnSparksPlayButton()
    {
        saprksPlay.SetActive(true);
    }

    public void Exit()
    {
        AudioManager.Instance.PlaySFX("Button");
        Debug.Log("Exit...");
        Application.Quit();
    }
}
