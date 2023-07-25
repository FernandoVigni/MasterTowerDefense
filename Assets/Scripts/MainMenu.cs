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
    [SerializeField] private GoldStatus goldStatus;
    [SerializeField] private GameObject buttonOptions;
    [SerializeField] public ProphecyScreen prophecyScreen;
    [SerializeField] public GameObject optionsInGameMenu;
    [SerializeField] public GameObject menuOptions;
    [SerializeField] public GameObject confirmation;
    [SerializeField] public GameObject victory;
    [SerializeField] public GameObject lose;
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject musicButtonOn;
    [SerializeField] public GameObject musicButtonOff;
    [SerializeField] public GameObject sfxButtonOn;
    [SerializeField] public GameObject sfxButtonOff;
    [SerializeField] public GameObject game;
    [SerializeField] public GameObject saprksPlay;
    [SerializeField] public GameObject saprksLetsGoButton;
    [SerializeField] public GameObject shoot;
    [SerializeField] public GameObject magicCircles;
    [SerializeField] public Tower tower;
    [SerializeField] public GameObject updateButtons;

   private EnemyManager enemyManager;
    private FireBallManager fireBallManager;
    public static MainMenu Instance;

    [SerializeField] public GameObject buttonPowerUp;
    [SerializeField] public GameObject buttonMinesDeploy;
    [SerializeField] public GameObject buttonHyperBeam;

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
        //goldStatus.gameObject.SetActive(false);
        enemyManager.RemoveAllInStage();
    }

    public void HandleButtons() 
    {
        updateButtons.SetActive(true);
        if (goldStatus.GetPowerUpValue() != 1 )
        {
            buttonPowerUp.SetActive(true);
        }
        else
        {
            if (goldStatus.GetMinesDeployValue() != 1) 
            {
                buttonMinesDeploy.SetActive(true);
            }
            else
            {
                if (goldStatus.GetHyperBeamValue() != 1)
                {
                    buttonHyperBeam.SetActive(true);
                }
                else
                {
                    buttonPowerUp.SetActive(false);
                    buttonMinesDeploy.SetActive(false);
                    buttonHyperBeam.SetActive(false);
                }
            }
        }
    }
    
    public void ValidatePowerUpUpdate()
    {
        if (goldStatus.currentGold >= goldStatus.habilityHandler.HabilityList[0].cost && goldStatus.powerUpUpdated == 0) //si tengo oro y no esta mejorado el powerUp
        {
            goldStatus.SetPowerUpTrue(); //cambiar en lo guardado en el telefono que esta aprendida la hab
            tower.ActivatePowerUp(); //prender las lucesitas y hacer el ataque mas rapido
            buttonPowerUp.gameObject.SetActive(false); //apagar el boton
            buttonMinesDeploy.SetActive(true); //prender el sigueinte  boton
        }
    }

    public void ValidateMinesDeploy()
    {
        if (goldStatus.currentGold >= goldStatus.habilityHandler.HabilityList[1].cost && goldStatus.minesDeployUpdate == 0)
        {
            goldStatus.SetMinesDeployTrue();
            buttonMinesDeploy.SetActive(false);
            buttonHyperBeam.SetActive(true);
        }
    }

    public void ValidateHyperBeam()
    {
        if (goldStatus.currentGold >= goldStatus.habilityHandler.HabilityList[2].cost && goldStatus.hyperBeamUpdate == 0)
        {
            goldStatus.SetHyperBeamTrue();
            buttonHyperBeam.SetActive(false);
        }
    }

    public void SetRenderCamera() 
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
    }
    
    public async void Play()
    {
        MainCamera.instance.TurnOffAllCameras();
        updateButtons.SetActive(false);
        enemyManager.DestroyAllEnemies();
        shoot.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
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
        goldStatus.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }
    public void Pause()
    {
        AudioManager.Instance.PlaySFX("Button");
        Time.timeScale = 0f;
        buttonOptions.SetActive(false);
        shoot.SetActive(false);
        optionsInGameMenu.SetActive(true);
        menuOptions.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySFX("Button");
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        shoot.SetActive(true);
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
        enemyManager.DestroyAllEnemies();

        EnterInMainMenu();
    }

    public void EnterInMainMenu()
    {
        Time.timeScale = 1f;
        HandleButtons();
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
