using UnityEngine;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject contact;
    [SerializeField] private GameObject volumen;
    [SerializeField] private GoldStatus goldStatus;
    [SerializeField] private GameObject buttonOptions;
    [SerializeField] public ProphecyScreen prophecyScreen;
    [SerializeField] public GameObject optionsButton; 
    [SerializeField] public GameObject goldStatusBox;
    [SerializeField] public GameObject menuOptions;
    [SerializeField] public GameObject confirmation;
    [SerializeField] public GameObject afterFadeVictory;
    [SerializeField] public GameObject lose;
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject musicButtonOn;
    [SerializeField] public GameObject musicButtonOff;
    [SerializeField] public GameObject sfxButtonOn;
    [SerializeField] public GameObject sfxButtonOff;
    [SerializeField] public GameObject game;
    [SerializeField] public GameObject saprksPlay;
    [SerializeField] private GameObject playClickAnimation;
    [SerializeField] public GameObject saprksLetsGoButton;
    [SerializeField] public GameObject shoot;
    [SerializeField] public GameObject magicCircles;
    [SerializeField] public Tower tower;
    [SerializeField] public UpdateButtons updateButtons;

    public bool isFinalAtackPresed;
    private EnemyManager enemyManager;
    private FireBallManager fireBallManager;
    public HabilityHandler habilityHandler;
    public GameObject EffectPowerUp;
    public GameObject EffectMines;
    public GameObject EffectFinalAtack;


    [SerializeField] public GameObject buttonPowerUp;
    [SerializeField] public GameObject buttonMinesDeploy;
    [SerializeField] public GameObject buttonHyperBeam;
    [SerializeField] public GameObject buttonMinesDeployInGame;
    [SerializeField] public GameObject buttonFinalAtackInGame;

    public void SetFinalAtackTrue() 
    {
        isFinalAtackPresed = true;
    }

    public void SetFinalAtackFalse()
    {
        isFinalAtackPresed = false;
    }

    public void TurnOnbuttonFinalAtackInGame() 
    {
        buttonFinalAtackInGame.SetActive(true);
    }

    public void TurnOffbuttonFinalAtackInGame()
    {
        buttonFinalAtackInGame.SetActive(false);
    }

    public void DestroyAllMines()
    {
        ExplosiveMine[] explosiveMines = FindObjectsOfType<ExplosiveMine>();
        foreach (ExplosiveMine mine in explosiveMines)
        {
            Destroy(mine.gameObject);
        }
    }

    public void TurnOffShootButton() 
    {
        shoot.SetActive(false);
    }

    public void TurnOnButtonMinesDeploy()
    {
        buttonMinesDeployInGame.SetActive(true);
        tower.ResetFirstActivationOfMinesButton();
    }

    public void TurnOffButtonMinesDeploy()
    {
        buttonMinesDeployInGame.SetActive(false);
    }

    public void TurnOnShootButton()
    {
        shoot.SetActive(true);
    }

    private void Awake()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        fireBallManager = FindAnyObjectByType<FireBallManager>();
        tower.towerFire.SetActive(false);

        optionsButton.SetActive(false);
        goldStatusBox.SetActive(false);
        //playClickAnimation.SetActive(false);

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
        enemyManager.RemoveAllInStage();
    }

    public void HandleButtons() 
    {
        updateButtons.gameObject.SetActive(true);
        updateButtons.SetValuesOfHabilities();

        if (goldStatus.GetPowerUpValue() != 1 )
        {
            buttonPowerUp.SetActive(true);
            if (goldStatus.currentGold > habilityHandler.GetHabilityCostByName("PowerUp")) 
            {
                EffectPowerUp.SetActive(true);
            }
        }
        else
        {
            if (goldStatus.GetMinesDeployValue() != 1) 
            {
                buttonMinesDeploy.SetActive(true);
                if (goldStatus.currentGold > habilityHandler.GetHabilityCostByName("ExplosiveMine"))
                {
                    EffectMines.SetActive(true);
                }
            }
            else
            {
                if (goldStatus.GetHyperBeamValue() != 1)
                {
                    buttonHyperBeam.SetActive(true);
                    if (goldStatus.currentGold > habilityHandler.GetHabilityCostByName("HyperBeam"))
                    {
                        EffectFinalAtack.SetActive(true);
                    }
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
        if (goldStatus.GetPowerUpValue() == 0 && goldStatus.currentGold > habilityHandler.GetHabilityCostByName("PowerUp"))
        {
            EffectPowerUp.SetActive(true);
            return;
        }
        else 
        {
            EffectPowerUp.SetActive(false);
        }
        if (goldStatus.GetMinesDeployValue() == 0 && goldStatus.currentGold > habilityHandler.GetHabilityCostByName("ExplosiveMine") && goldStatus.GetPowerUpValue() == 1) 
        {
            EffectMines.SetActive(true);
            return;
        }
        else
        {
            EffectMines.SetActive(false);
        }

        if (goldStatus.GetHyperBeamValue() == 0 && goldStatus.currentGold > habilityHandler.GetHabilityCostByName("HyperBeam") && goldStatus.GetMinesDeployValue() == 1 && goldStatus.GetPowerUpValue() == 1) 
        {
            EffectFinalAtack.SetActive(true);
            return;
        }
        else
        {
            EffectFinalAtack.SetActive(false);
        }

        DestroyAllMines();
        buttonMinesDeploy.SetActive(false);
        enemyManager.RemoveAllLists();
        tower.life = tower.maxLife;
        tower.towerFire.SetActive(false);
        AudioManager.Instance.PlaySFX("PlaySFXPlayButton");
        await Task.Delay(1500);
        MainCamera.instance.TurnOffAllCameras();
        updateButtons.gameObject.SetActive(false);
        enemyManager.DestroyAllEnemies();
        shoot.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
        loading.SetActive(true);
        MainMenu.Instance.TurnOffContactAndVolumen();
        magicCircles.SetActive(true);
        TurnOffSparksPlayButton();
        mainMenu.SetActive(false);
        await Task.Delay(5500);
        magicCircles.SetActive(false);
        loading.SetActive(false);
        prophecyScreen.gameObject.SetActive(true);
        prophecyScreen.StartProphecyScene();
        buttonOptions.gameObject.SetActive(true);
        goldStatus.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX("Button");
        Time.timeScale = 0f;
        optionsButton.SetActive(false);
        goldStatusBox.SetActive(false);
        shoot.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySFX("Button");
        menuOptions.SetActive(false);
        buttonOptions.SetActive(true);
        optionsButton.SetActive(true);
        goldStatusBox.SetActive(true);
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
        shoot.SetActive(false);
        contact.SetActive(false);
        confirmation.SetActive(false);
        volumen.SetActive(false);
        TurnOnSparksPlayButton();
    }
    
    public void CheckReturnToMainMenu() 
    {
        AudioManager.Instance.PlaySFX("Button");
        confirmation.SetActive(true);
        tower.ResetCore();
    }

    public void CloseConfirmation() 
    {
        shoot.SetActive(false);
        tower.towerFire.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
        confirmation.SetActive(false);
        DestroyAllMines();
    }

    public void TurnOffContactAndVolumen() 
    {
        contact.SetActive(false);
        volumen.SetActive(false);
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
        DestroyAllMines();
        optionsButton.SetActive(false);
        goldStatusBox.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
        enemyManager.RemoveAllInCollider();
        enemyManager.RemoveAllLists();
        enemyManager.DestroyAllEnemies();
        TurnOffCLicPlayAnimation();
        PhaseManager.instance.WinScreen.SetActive(false);
        PhaseManager.instance.loseScreen.SetActive(false);
        EnterInMainMenu();
    }

    public void EnterInMainMenu()
    {
        DestroyAllMines();
        tower.towerFire.SetActive(false);
        Time.timeScale = 1f;
        HandleButtons();
        CleanUiInGame();
        CloseMenuesUi();
        TurnOffScenario();
        TurnOnMainMenu();
        AudioManager.Instance.PlayMusic("MainMenu");
        TurnOnSparksPlayButton();
        prophecyScreen.TurnOffLetsGoAnimationOnCLick();
        PhaseManager.instance.fadeInOut.SetActive(false);
    }

    public void Lose() 
    {
        TurnOffButtonMinesDeploy();
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

    public void StartCLicPlayAnimation() 
    {
        playClickAnimation.SetActive(false);
        playClickAnimation.SetActive(true);
    }

    public void TurnOffCLicPlayAnimation()
    {
        playClickAnimation.SetActive(false);
    }

    public void Exit()
    {
        AudioManager.Instance.PlaySFX("Button");
        Debug.Log("Exit...");
        Application.Quit();
    }
}
