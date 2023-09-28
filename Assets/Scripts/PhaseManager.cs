using System.Threading.Tasks;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    public EnemyManager enemyManager;
    public Transform pointOfSpawnOfWave;
    public Transform BlueSpawnPosition;
    private AudioManager audioManager;
    public PortalsManager portals;
    public GameObject leftRocksToDestroy;
    public GameObject rightRocksToDestroy;
    public GameObject leftExplosion;
    public GameObject rightExplosion;
    public GameObject meteorites;
    public GameObject flashMeteorites;
    public GameObject DeathExplosion;
   // public Runner runnerForAnimation;
    public GameObject blueDragon;
    public Necromancer necromaner;
    public GameObject necromanerScene;  
    public int currentPhase;
    public float waveLimitTime;
    public GameObject enemiesPhaseOne;
    public GameObject enemiesPhaseTwo;
    public DragonController dragonController;
    public GoldStatus goldManager;
    public GameObject orcShaman;
    public bool isFirstProphecy;

    public CanvaMovementObjet canvaLeverOneWhite;
    public CanvaMovementObjet canvaLeverOneBlack;
    public CanvaMovementObjet canvaLeverTwoWhite;
    public CanvaMovementObjet canvaLeverTwoBlack;
    public CanvaMovementObjet canvaLeverThreeWhite;
    public CanvaMovementObjet canvaLeverThreeBlack;
    public CanvaMovementObjet canvaLeverFinalWhite;
    public CanvaMovementObjet canvaLeverFinalBlack;

    public GameObject winScreen;
    public GameObject loseScreen;

    //[SerializeField] public GameObject MinesDeploy;
   
    // Test phases
    float[] coefficient =            { 1.2f, 1.5f, 2, 0 };
    int[] amountOfBagOfGoldByPhase = { 10, 2, 8, 0 };
    int[] amountOfWarriosByPhase =   { 5, 3, 5, 0 };
    int[] amountOfMagesByPhase =     { 2, 7, 5, 0 };
    //int[] amountOfRunnersByPhase =   { 0, 0, 0, 0 };
    int[] amountOfGiantsByPhase =    { 2, 0, 5, 0 }; 

    string[] songsNames = { "MainMenu", "Phase0", "Phase1", "Phase2", "Phase3", "Victory", "Lose" };
    // PlaceHolders de , "AnimationPhase´s"
    //string[] songsNames = { "MainMenu","AnimationPhase0", "Phase0", "AnimationPhase1", "Phase1", "AnimationPhase2", "Phase2", "AnimationPhaseBoss", "PhaseBoss" };

    //----------

    private void Awake()
    {
        //Runner runner = GetComponent<Runner>();
        enemyManager = FindObjectOfType<EnemyManager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        ResetPhase();
    }

    public void ResetPhase() 
    {
        currentPhase = 0;
    }

    public void TurnOnRocksToDestroy()
    {
        leftRocksToDestroy.SetActive(true);
        rightRocksToDestroy.SetActive(true);
    }

    public float GetCoefficient()
    {
        return coefficient[currentPhase];
    }

    public void SetPhasePlusOne()
    {
        currentPhase += 1;
    }

    public string GetCurrentPhaseName()
    {
        string name = songsNames[currentPhase];
        return name;
    }

    public void PlayMusic()
    {
        string name = songsNames[currentPhase + 1];
        audioManager.PlayMusic(name);
    }

    public async Task ActivateAnimationPhaseOne()
    {
        ResetPhase();
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        MainMenu.Instance.shoot.SetActive(false);
        dragonController.ResetDragon();
        AudioManager.Instance.PlayMusic("Intro");
        necromanerScene.SetActive(false);
        orcShaman.SetActive(true);
        flashMeteorites.SetActive(false);
        MainCamera.instance.TurnOffPhaseOneCameras();
        portals.TurnOffPortals();
        MainCamera.instance.cameraBaseEndInChaman.SetActive(true);
        await Task.Delay(6000);
        meteorites.SetActive(true);
        await Task.Delay(3200);
        MainCamera.instance.cameraBaseEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(true);
        await Task.Delay(1500);
        orcShaman.SetActive(false);
        await Task.Delay(4500);
        leftExplosion.SetActive(true);
        AudioManager.Instance.PlaySFX("Explosion0");
        await Task.Delay(600);
        leftRocksToDestroy.SetActive(false);
        await Task.Delay(600);
        rightExplosion.SetActive(true);
        AudioManager.Instance.PlaySFX("Explosion0");
        await Task.Delay(400);
        rightRocksToDestroy.SetActive(false);
        flashMeteorites.SetActive(true);
        await Task.Delay(400);
        leftExplosion.SetActive(false);
        meteorites.SetActive(false);
        AudioManager.Instance.PlaySFX("WindOutside");
        await Task.Delay(2000);
        flashMeteorites.SetActive(false);
        rightExplosion.SetActive(false);
        orcShaman.SetActive(true);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(false);
        MainCamera.instance.portalsEndInChaman.SetActive(true);
        await Task.Delay(4000);
        PlayMusic();
        portals.TurnOnLeftPortal();
        //TurnOnWinScreen();


        canvaLeverOneWhite.gameObject.SetActive(true);
        canvaLeverOneBlack.gameObject.SetActive(true);
        canvaLeverOneWhite.FadeOutAndDeactivate();
        canvaLeverOneBlack.FadeOutAndDeactivate();

        await Task.Delay(2500);
        LoadEnemies();
        MainMenu.Instance.shoot.SetActive(true);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        MainCamera.instance.portalsEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanEndInTowerLeft.SetActive(true);
        enemyManager.SendEnemiesLeftPortal();
        await Task.Delay(1500);
        orcShaman.SetActive(false);
    }

    public async Task ActivateAnimationPhaseTwo()
    {
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        MainMenu.Instance.shoot.SetActive(false);
        MainCamera.instance.TurnOffPhaseTwoCameras();
        MainCamera.instance.towerLeft.SetActive(true);
        await Task.Delay(4000);
        MainCamera.instance.towerLeft.SetActive(false);
        MainCamera.instance.cameraFrontRunner.SetActive(true);
        await Task.Delay(4000);
        MainCamera.instance.cameraFrontRunner.SetActive(false);
        MainCamera.instance.camera3PersonRunner.SetActive(true);
        orcShaman.SetActive(true);
        LoadEnemies();
        await Task.Delay(2000);
        await Task.Delay(1500);
        portals.TurnOnRightPortal();
        await Task.Delay(1000);

        canvaLeverTwoWhite.gameObject.SetActive(true);
        canvaLeverTwoBlack.gameObject.SetActive(true);
        canvaLeverTwoWhite.FadeOutAndDeactivate();
        canvaLeverTwoBlack.FadeOutAndDeactivate();
        await Task.Delay(2000);
        MainMenu.Instance.shoot.SetActive(true);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        enemyManager.SendEnemiesRightPortal();
        await Task.Delay(2000);
        MainCamera.instance.camera3PersonRunner.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(true);
        await Task.Delay(1000);
        orcShaman.SetActive(false);
        await Task.Delay(1500);
    }

    public async Task ActivateAnimationPhaseThree()
    {
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        MainMenu.Instance.shoot.SetActive(false);
        await Task.Delay(2000);
        PlayMusic();
        MainCamera.instance.TurnOffPhaseThreeCameras();
        necromanerScene.SetActive(true);
        enemiesPhaseOne.SetActive(true);
        enemiesPhaseTwo.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(false);
        MainCamera.instance.camera3PersonTowerRight.SetActive(true);
        await Task.Delay(3000);
        DeathExplosion.SetActive(true);
        await Task.Delay(300);
        enemiesPhaseOne.SetActive(false);
        enemiesPhaseTwo.SetActive(true);
        await Task.Delay(1000);
        DeathExplosion.SetActive(false);
        await Task.Delay(3000);
        MainCamera.instance.camera3PersonTowerRight.SetActive(false);
        MainCamera.instance.cameraNecromancer.SetActive(true);
        await Task.Delay(1500);
        necromanerScene.SetActive(false);
        MainMenu.Instance.shoot.SetActive(true);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        LoadEnemies();
        enemyManager.SendEnemiesRightPortal();
        canvaLeverThreeWhite.gameObject.SetActive(true);
        canvaLeverThreeBlack.gameObject.SetActive(true);
        canvaLeverThreeWhite.FadeOutAndDeactivate();
        canvaLeverThreeBlack.FadeOutAndDeactivate();
        CheckMinesActives();
    }

    public void CheckMinesActives() 
    {
        if (goldManager.minesDeployUpdate == 1)
        {
            MainMenu.Instance.TurnOnButtonMinesDeploy();
            MainMenu.Instance.TurnOffshootButton();
        }
    }

    public async Task ActivateAnimationPhaseFour()
    {
        MainCamera.instance.cameraNecromancer.SetActive(false);
        MainCamera.instance.TurnOffPhaseFourCameras();
        blueDragon.transform.position = BlueSpawnPosition.position;
        blueDragon.SetActive(true);
        MainCamera.instance.dragonCamera.SetActive(true);
        await Task.Delay(1500);
        canvaLeverFinalWhite.gameObject.SetActive(true);
        canvaLeverFinalBlack.gameObject.SetActive(true);
        canvaLeverFinalWhite.FadeOutAndDeactivate();
        canvaLeverFinalBlack.FadeOutAndDeactivate();
    }

    public void SetCurrentPhase0()
    {
        currentPhase = 0;
    }

    public void StartPhase() 
    {
        switch (currentPhase)
        {
            case 0:
                ActivateAnimationPhaseOne();
                break;
            case 1:
                ActivateAnimationPhaseTwo();
                break;
            case 2:
                ActivateAnimationPhaseThree();
                break;
            case 3:
                ActivateAnimationPhaseFour();
                break;
            // Agrega más casos según la cantidad de fases que tengas
            default:
                Debug.LogError("Invalid phase number");
                break;
        }
    }

    public bool nextPhase() 
    {
        if (currentPhase < amountOfBagOfGoldByPhase.Length)
            { return true ; }
        else
            { return false; }
    }

    public int GetAmountOfPhases() 
    {
        int amountOfPhases = amountOfWarriosByPhase.Length;
        return amountOfPhases;
    }

    public float GetAmountBagOfGold() 
    {
        return amountOfBagOfGoldByPhase[currentPhase];
    }

    public void TurnOnWinScreen() 
    {
        MainMenu.Instance.shoot.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 1f;
    }

    public void TurnOnLoseScreen()
    {
        Time.timeScale = 0f;
        MainMenu.Instance.TurnOffButtonMinesDeploy();
        ResetPhase();
        MainMenu.Instance.shoot.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void TurnOffWinScreen()
    {
        winScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TurnOffLoseScreen()
    {
        loseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadEnemies()
    {
        if (amountOfWarriosByPhase.Length >= currentPhase)
        {
            for (int i = 0; i < amountOfWarriosByPhase[currentPhase]; i++)
            { enemyManager.InstantiateWarrior(); }

            for (int i = 0; i < amountOfMagesByPhase[currentPhase]; i++)
            { enemyManager.InstantiateMage(); }

            for (int i = 0; i < amountOfGiantsByPhase[currentPhase]; i++)
            { enemyManager.InstantiateGiant(); }
        }
    }
}
