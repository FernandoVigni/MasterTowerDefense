using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;



    public Tower tower;
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
    public GameObject loseScreen;
    public GameObject WinScreen;
    public GameObject skipAnimation;
    public bool endPhaseOneActions;
    private CancellationToken cancellationToken;
    private CancellationTokenSource sharedCancellationTokenSource;


    public CanvaMovementObjet canvaLeverOneWhite;
    public CanvaMovementObjet canvaLeverOneBlack;
    public CanvaMovementObjet canvaLeverTwoWhite;
    public CanvaMovementObjet canvaLeverTwoBlack;
    public CanvaMovementObjet canvaLeverThreeWhite;
    public CanvaMovementObjet canvaLeverThreeBlack;
    public CanvaMovementObjet canvaLeverFinalWhite;
    public CanvaMovementObjet canvaLeverFinalBlack;
    public float cooldownDuration; 
    private float cooldownTimer;
    public GameObject loseFadeInOut;
    public GameObject winFadeInOut;

    //Definitive Matrix
        float[] coefficient =            { 1.2f, 1.5f, 2, 0 };
        int[] amountOfBagOfGoldByPhase = { 10, 2, 8, 0 };
        int[] amountOfWarriosByPhase =   { 3, 3, 5, 0 };
        int[] amountOfMagesByPhase =     { 2, 5, 5, 0 };
        int[] amountOfGiantsByPhase =    { 2, 0, 5, 0 };


    /*
    float[] coefficient = { 1.2f, 1.5f, 2, 0 };
    int[] amountOfBagOfGoldByPhase = { 10, 2, 8, 0 };
    int[] amountOfWarriosByPhase = { 1, 0, 0, 0 };
    int[] amountOfMagesByPhase = { 0, 1, 0, 0 };
    int[] amountOfGiantsByPhase = { 0, 0, 1, 0 };
    */


    string[] songsNames = { "MainMenu", "Phase0", "Phase1", "Phase2", "Phase3", "Victory", "Lose" };

    private void Awake()
    {
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

    public void ResetLevelsLabels()
    {
        canvaLeverOneWhite.gameObject.SetActive(false);
        canvaLeverOneBlack.gameObject.SetActive(false);

        canvaLeverTwoWhite.gameObject.SetActive(false);
        canvaLeverTwoBlack.gameObject.SetActive(false);

        canvaLeverThreeWhite.gameObject.SetActive(false);
        canvaLeverThreeBlack.gameObject.SetActive(false);

        canvaLeverFinalWhite.gameObject.SetActive(false);
        canvaLeverFinalBlack.gameObject.SetActive(false);



    }

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        // Crea un token de cancelaci�n que estar� vinculado al ciclo de vida de este objeto.
        cancellationToken = new CancellationToken();
        sharedCancellationTokenSource = new CancellationTokenSource();
        cancellationToken = sharedCancellationTokenSource.Token;
        ResetPhase();
    }

    private void Update()
    {
        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0.0f)
            {
                cooldownTimer = 0.0f;
            }
        }
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

    public async Task ActivateAnimationPhaseOne(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested){return;}
        dragonController.ResetWayPoints();
        MainMenu.Instance.firstWin = true;
        ResetLevelsLabels();
        tower.basicCore.SetActive(true);
        tower.finalBigCore.SetActive(true);
        MainMenu.Instance.SetFinalAtackFalse();
        tower.firstEndGame = true;
        ResetPhase();
        tower.TurnOffLifeBarCanva();
        tower.ResetCore();
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
        await Task.Delay(3000, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        //skipAnimation.SetActive(true);
        await Task.Delay(3000, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        meteorites.SetActive(true);
        await Task.Delay(3200, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        MainCamera.instance.cameraBaseEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(true);
        await Task.Delay(1500, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        orcShaman.SetActive(false);
        await Task.Delay(4500, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        skipAnimation.SetActive(false);
        leftExplosion.SetActive(true);
        AudioManager.Instance.PlaySFX("Explosion0");
        await Task.Delay(600, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        leftRocksToDestroy.SetActive(false);
        await Task.Delay(600, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        rightExplosion.SetActive(true);
        AudioManager.Instance.PlaySFX("Explosion0");
        await Task.Delay(400, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        rightRocksToDestroy.SetActive(false);
        flashMeteorites.SetActive(true);
        await Task.Delay(400, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        leftExplosion.SetActive(false);
        meteorites.SetActive(false);
        AudioManager.Instance.PlaySFX("WindOutside");
        skipAnimation.SetActive(false);
        await Task.Delay(2000, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        skipAnimation.SetActive(false);
        flashMeteorites.SetActive(false);
        rightExplosion.SetActive(false);
        orcShaman.SetActive(true);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(false);
        MainCamera.instance.portalsEndInChaman.SetActive(true);
        await Task.Delay(4000, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        PlayMusic();
        portals.TurnOnLeftPortal();
        canvaLeverOneWhite.gameObject.SetActive(true);
        canvaLeverOneBlack.gameObject.SetActive(true);
        canvaLeverOneWhite.FadeOutAndDeactivate();
        canvaLeverOneBlack.FadeOutAndDeactivate();
        await Task.Delay(2500, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        LoadEnemies();
        MainMenu.Instance.shoot.SetActive(true);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        MainCamera.instance.portalsEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanEndInTowerLeft.SetActive(true);
        enemyManager.SendEnemiesLeftPortal();
        tower.TurnOnLifeBarCanva();
        await Task.Delay(1500, cancellationToken);
        if (cancellationToken.IsCancellationRequested) { return; }
        orcShaman.SetActive(false);
    }


    public void SkipAnimationPhaseOne() 
    {

        skipAnimation.SetActive(false);
        loseFadeInOut.SetActive(false);
        loseFadeInOut.SetActive(true);
        meteorites.SetActive(false);
        MainCamera.instance.TurnOffPhaseOneCameras();
        ResetPhase();
        tower.TurnOffLifeBarCanva();
        tower.ResetCore();
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        MainMenu.Instance.shoot.SetActive(false);
        dragonController.ResetDragon();
        necromanerScene.SetActive(false);
        leftRocksToDestroy.SetActive(false);
        rightRocksToDestroy.SetActive(false);
        leftExplosion.SetActive(false);
        meteorites.SetActive(false);
        flashMeteorites.SetActive(false);
        rightExplosion.SetActive(false);
        skipAnimation.SetActive(false);
        orcShaman.SetActive(true);
        PlayMusic();
        portals.TurnOnLeftPortal();
        canvaLeverOneWhite.gameObject.SetActive(true);
        canvaLeverOneBlack.gameObject.SetActive(true);
        canvaLeverOneWhite.FadeOutAndDeactivate();
        canvaLeverOneBlack.FadeOutAndDeactivate();
        LoadEnemies();
        skipAnimation.SetActive(false);
        MainMenu.Instance.shoot.SetActive(true);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        MainCamera.instance.portalsEndInChaman.SetActive(false);
        skipAnimation.SetActive(false);
        EndOfSkipAnimation();
    }

    public async Task EndOfSkipAnimation() 
    {
        meteorites.SetActive(false);
        leftExplosion.SetActive(false);
        rightExplosion.SetActive(false);
        await Task.Delay(1500);
        SkipAnimation();
        MainCamera.instance.TurnOffPhaseOneCameras();
        MainCamera.instance.cameraChamanEndInTowerLeft.SetActive(true);
        await Task.Delay(1500);
        enemyManager.SendEnemiesLeftPortal();
        tower.TurnOnLifeBarCanva();
        orcShaman.SetActive(false);
        loseFadeInOut.SetActive(false);
    }

    public void SkipAnimation()
    {
        if (cooldownTimer <= 0.0f)
        {
            skipAnimation.SetActive(false);
            cooldownTimer = cooldownDuration;
        }
    }


    public async Task ActivateAnimationPhaseTwo()
    {
        meteorites.SetActive(false);
        tower.TurnOffLifeBarCanva();
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
        enemyManager.SendEnemiesRightPortal();
        tower.TurnOnLifeBarCanva();
        await Task.Delay(2000);
        MainCamera.instance.camera3PersonRunner.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(true);
        await Task.Delay(1000);
        orcShaman.SetActive(false);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
    }

    public async Task ActivateAnimationPhaseThree()
    {
        tower.TurnOffLifeBarCanva();
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        MainMenu.Instance.shoot.SetActive(false);
        await Task.Delay(2000);
        PlayMusic();
        MainCamera.instance.TurnOffPhaseThreeCameras();
        necromanerScene.SetActive(true);
        enemiesPhaseOne.SetActive(true);
        necromaner.AllIdle();

        enemiesPhaseTwo.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(false);
        MainCamera.instance.camera3PersonTowerRight.SetActive(true);
        await Task.Delay(3000);
        DeathExplosion.SetActive(true);
        await Task.Delay(300);
        enemiesPhaseOne.SetActive(false);
        enemiesPhaseTwo.SetActive(true);
        necromaner.AllIdle();
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
        tower.TurnOnLifeBarCanva();
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
            MainMenu.Instance.TurnOffShootButton();
        }
    }

    public async Task ActivateAnimationPhaseFour()
    {
        tower.TurnOffLifeBarCanva();
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

    public async Task StartPhase() 
    {
        switch (currentPhase)
        {
            case 0:
                ActivateAnimationPhaseOne(cancellationToken);
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
            // Agrega m�s casos seg�n la cantidad de fases que tengas
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
