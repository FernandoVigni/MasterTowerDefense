using Broccoli.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

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
    private GameObject game;
    public GameObject meteorites;
    public GameObject flashMeteorites;
    public Runner runner;
    private Tower towerScript;
    public Necromancer necromaner;
    public int currentPhase;
    public float waveLimitTime;



    //----------
    int[] coefficient = { 1, 1, 1, 2 };
    int[] amountOfBagOfGoldByPhase = { 1, 1, 5, 5 };
    int[] amountOfWarriosByPhase = { 1, 1, 5, 5 };
    int[] amountOfMagesByPhase = { 1, 1, 5, 5 };
    int[] amountOfRunnersByPhase = { 0, 5, 0, 0 };
    int[] amountOfGiantsByPhase = { 1, 1, 6, 3 };
    int[] amountOfBosses = { 0, 0, 0, 1 };

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
        PhaseManager.instance.portals.TurnOffPortals();
        MainCamera.instance.cameraBaseEndInChaman.SetActive(true);

        await Task.Delay(6000);
        meteorites.SetActive(true);
        await Task.Delay(2000);
        MainCamera.instance.cameraBaseEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(true);
        await Task.Delay(6000);
        leftExplosion.SetActive(true);
        await Task.Delay(600);
        leftRocksToDestroy.SetActive(false);
        await Task.Delay(1600);
        leftExplosion.SetActive(false);
        rightExplosion.SetActive(true);
        await Task.Delay(400);
        rightRocksToDestroy.SetActive(false);
        flashMeteorites.SetActive(true);
        await Task.Delay(400);
        meteorites.SetActive(false);
        await Task.Delay(2000);
        flashMeteorites.SetActive(false);
        rightExplosion.SetActive(false);
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

    }

    public async Task ActivateAnimationPhaseTwo()
    {
        runner.gameObject.SetActive(true);
        MainCamera.instance.towerLeft.SetActive(true);
        await Task.Delay(4000);
        MainCamera.instance.towerLeft.SetActive(false);
        MainCamera.instance.cameraFrontRunner.SetActive(true);
        runner.isWalking = true;
        await Task.Delay(7000);
        MainCamera.instance.cameraFrontRunner.SetActive(false);
        MainCamera.instance.camera3PersonRunner.SetActive(true);
        PlayMusic();
        LoadEnemies();
        await Task.Delay(2000);
        enemyManager.RemoveEnemyFromSentList(runner);
        enemyManager.RemoveEnemiesInsideColliderList(runner);
        runner.gameObject.SetActive(false);
        await Task.Delay(1500);
        portals.TurnOnRightPortal();
        portals.TurnOffLeftPortal();
        await Task.Delay(1000);
        enemyManager.SendEnemiesRightPortal();
        await Task.Delay(3000);
        MainCamera.instance.camera3PersonRunner.SetActive(false);
        MainCamera.instance.cameraChamanAndPortal.SetActive(true);
        await Task.Delay(5000);
    }

    public async Task ActivateAnimationPhaseThree()
    {
        MainCamera.instance.cameraChamanAndPortal.SetActive(false);
        MainCamera.instance.camera3PersonTowerRight.SetActive(true);
        await Task.Delay(3000);

        necromaner.Atack();
        Debug.Log("anim necromancer");
        await Task.Delay(3000);
        // animacion bichos lvl up
        Debug.Log("bichos lvl up");
        await Task.Delay(3000);
        necromaner.Idle();
        MainCamera.instance.camera3PersonTowerRight.SetActive(false);
        MainCamera.instance.cameraNecromancer.SetActive(true);
    }


    public async Task ActivateAnimationPhaseFour()
    {

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
