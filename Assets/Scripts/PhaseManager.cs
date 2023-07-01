using Broccoli.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int currentPhase;
    public float waveLimitTime;



    //----------
    int[] coefficient =              { 1, 1, 1, 2 };
    int[] amountOfBagOfGoldByPhase = { 1, 2, 5, 5 };
    int[] amountOfWarriosByPhase =   { 5, 3, 5, 5 };
    int[] amountOfMagesByPhase =     { 2, 5, 5, 5 };
    int[] amountOfRunnersByPhase =   { 0, 40, 0, 0 };
    int[] amountOfGiantsByPhase =    { 2, 2, 6, 3 };
    int[] amountOfBosses = { 0, 0, 0, 1 };

    string[] songsNames = { "MainMenu", "Phase0", "Phase1", "Phase2", "PhaseBoss", "Victory", "Lose" };
    // PlaceHolders de , "AnimationPhase´s"
    //string[] songsNames = { "MainMenu","AnimationPhase0", "Phase0", "AnimationPhase1", "Phase1", "AnimationPhase2", "Phase2", "AnimationPhaseBoss", "PhaseBoss" };

    //----------


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

        await Task.Delay(8000);
        meteorites.SetActive(true);
        await Task.Delay(2000);
        MainCamera.instance.cameraBaseEndInChaman.SetActive(false);
        MainCamera.instance.cameraChamanBaseEndInPortals.SetActive(true);
        await Task.Delay(6000);
        leftExplosion.SetActive(true);
        await Task.Delay(1200);
        leftRocksToDestroy.SetActive(false);
        await Task.Delay(1000);
        leftExplosion.SetActive(false);
        rightExplosion.SetActive(true);
        await Task.Delay(1200);
        rightRocksToDestroy.SetActive(false);
        meteorites.SetActive(false);
        await Task.Delay(1200);
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
        MainCamera.instance.cameraFrontRunner.SetActive(true);

        await Task.Delay(3000);
        MainCamera.instance.camera3PersonRunner.SetActive(true);

        await Task.Delay(5000);
        MainCamera.instance.cameraChamanAndPortal.SetActive(true);
        await Task.Delay(3000);
 
        portals.TurnOnRightPortal();
        portals.TurnOffLeftPortal();
        await Task.Delay(3000);
        PlayMusic();
        MainCamera.instance.camera3PersonTowerRight.SetActive(true);

        LoadEnemies();
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
