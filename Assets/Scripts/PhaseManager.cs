using System.Threading.Tasks;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    public EnemyManager enemyManager;
    public Transform pointOfSpawnOfWave;
    private AudioManager audioManager;
    public PortalsManager portals;
    public GameObject leftRocksToDestroy;
    public GameObject rightRocksToDestroy;
    public GameObject leftExplosion;
    public GameObject rightExplosion;
    public GameObject meteorites;
    public GameObject flashMeteorites;
    public GameObject DeathExplosion;
    public Runner runnerForAnimation;
    public GameObject blueDragon;
    public Necromancer necromaner;
    public GameObject necromanerScene;  
    public int currentPhase;
    public float waveLimitTime;
    public GameObject enemiesPhaseOne;
    public GameObject enemiesPhaseTwo;
    public DragonController dragon;
    public GoldStatus goldStatus;
    public GameObject orcShaman;

    [SerializeField] public GameObject MinesDeploy;

    //----------
    //Real Phases
    /*
    int[] coefficient =              { 1, 1, 2, 2 };
    int[] amountOfBagOfGoldByPhase = { 1, 1, 3, 5 };
    int[] amountOfWarriosByPhase =   { 7, 6, 7, 7 };
    int[] amountOfMagesByPhase =     { 5, 3, 4, 5 };
    int[] amountOfRunnersByPhase =   { 0, 6, 0, 0 };
    int[] amountOfGiantsByPhase =    { 2, 0, 3, 3 };
    */


    
    // Test phases
    int[] coefficient =              { 1, 1, 2, 2 };
    int[] amountOfBagOfGoldByPhase = { 1, 1, 2, 2 };
    int[] amountOfWarriosByPhase =   { 2, 2, 2, 2 };
    int[] amountOfMagesByPhase =     { 2, 2, 2, 2 };
    int[] amountOfRunnersByPhase =   { 0, 0, 0, 0 };
    int[] amountOfGiantsByPhase =    { 1, 0, 2, 2 }; 


    

    string[] songsNames = { "MainMenu", "Phase0", "Phase1", "Phase2", "PhaseBoss", "Victory", "Lose" };
    // PlaceHolders de , "AnimationPhase´s"
    //string[] songsNames = { "MainMenu","AnimationPhase0", "Phase0", "AnimationPhase1", "Phase1", "AnimationPhase2", "Phase2", "AnimationPhaseBoss", "PhaseBoss" };

    //----------

    private void Awake()
    {
        Runner runner = GetComponent<Runner>();
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

    public int GetCoefficient()
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
        dragon.ResetDragon();
        //ResetDragon();
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
        await Task.Delay(1600);
        leftExplosion.SetActive(false);
        rightExplosion.SetActive(true);
        AudioManager.Instance.PlaySFX("Explosion0");
        await Task.Delay(400);
        rightRocksToDestroy.SetActive(false);
        flashMeteorites.SetActive(true);
        await Task.Delay(400);
        meteorites.SetActive(false);
        await Task.Delay(2000);
        flashMeteorites.SetActive(false);
        rightExplosion.SetActive(false);
        orcShaman.SetActive(true);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(false);
        MainCamera.instance.portalsEndInChaman.SetActive(true);
        await Task.Delay(4000);
        PlayMusic();
        portals.TurnOnLeftPortal();
        await Task.Delay(2500);
        MainMenu.Instance.shoot.SetActive(true);
        MainCamera.instance.portalsEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanEndInTowerLeft.SetActive(true);
        LoadEnemies();
        enemyManager.SendEnemiesLeftPortal();
        await Task.Delay(1500);
        MainMenu.Instance.optionsButton.SetActive(true);
        MainMenu.Instance.goldStatusBox.SetActive(true);
        orcShaman.SetActive(false);
    }

    public async Task ActivateAnimationPhaseTwo()
    {
        MainCamera.instance.TurnOffPhaseTwoCameras();
        runnerForAnimation.gameObject.SetActive(true);
        MainCamera.instance.towerLeft.SetActive(true);
        runnerForAnimation.Scream();
        await Task.Delay(4000);
        MainCamera.instance.towerLeft.SetActive(false);
        MainCamera.instance.cameraFrontRunner.SetActive(true);
        //runnerForAnimation.isWalking = true;    si activo esto se rompe y me tira que no es una instancia o algo asi.
        await Task.Delay(7000);
        MainCamera.instance.cameraFrontRunner.SetActive(false);
        MainCamera.instance.camera3PersonRunner.SetActive(true);
        orcShaman.SetActive(true);
        PlayMusic();
        LoadEnemies();
        await Task.Delay(2000);
        enemyManager.RemoveEnemiesInsideColliderList(runnerForAnimation);
        runnerForAnimation.gameObject.SetActive(false);
        await Task.Delay(1500);
        portals.TurnOnRightPortal();
        await Task.Delay(1000);
        enemyManager.SendEnemiesRightPortal();
        await Task.Delay(3000);
        orcShaman.SetActive(false);
        MainCamera.instance.camera3PersonRunner.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(true);
        await Task.Delay(5000);
    }

    public async Task ActivateAnimationPhaseThree()
    {
        MainCamera.instance.TurnOffPhaseThreeCameras();
        necromanerScene.SetActive(true);
        enemiesPhaseOne.SetActive(true);
        enemiesPhaseTwo.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(false);
        MainCamera.instance.camera3PersonTowerRight.SetActive(true);
        //necromaner.Atack();
        await Task.Delay(3000);
        DeathExplosion.SetActive(true);
        await Task.Delay(300);
        enemiesPhaseOne.SetActive(false);
        enemiesPhaseTwo.SetActive(true);
        await Task.Delay(1000);
        DeathExplosion.SetActive(false);
        await Task.Delay(3000);
        //necromaner.Idle();
        MainCamera.instance.camera3PersonTowerRight.SetActive(false);
        MainCamera.instance.cameraNecromancer.SetActive(true);
        necromanerScene.SetActive(false);

        LoadEnemies();
        enemyManager.SendEnemiesRightPortal();
        CheckMinesActives();
    }



    public void CheckMinesActives() 
    {
        if (goldStatus.minesDeployUpdate == 1)
        {
            MinesDeploy.SetActive(true);
        }
        else
        {
            MinesDeploy.SetActive(false);
        }
    }

    public async Task ActivateAnimationPhaseFour()
    {
        MainCamera.instance.cameraNecromancer.SetActive(false);
        MainCamera.instance.TurnOffPhaseFourCameras();
        blueDragon.transform.position = new Vector3(240f, 22f, 300f);
        blueDragon.SetActive(true);
        MainCamera.instance.dragonCamera.SetActive(true);

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

            for (int i = 0; i < amountOfRunnersByPhase[currentPhase]; i++)
            { enemyManager.InstantiateRunner(); }

            //enemyManager.SendEnemiesLeftPortal();
        }
    }
}
