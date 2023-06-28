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
    public static PhaseManager Instance;

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

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>(); 
        currentPhase = 0;
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
        MainCamera.instance.camera360ToChaman.SetActive(true);
        await Task.Delay(8000);
        meteorites.SetActive(true);
        await Task.Delay(2000);
        MainCamera.instance.cameraChamanToPortals.SetActive(true);
        await Task.Delay(6000);
        leftExplosion.SetActive(true);
        await Task.Delay(1500);
        leftRocksToDestroy.SetActive(false);
        await Task.Delay(1000);
        leftExplosion.SetActive(false);
        rightExplosion.SetActive(true);
        await Task.Delay(1500);
        rightRocksToDestroy.SetActive(false);
        await Task.Delay(1000);
        rightExplosion.SetActive(false);

        MainCamera.instance.cameraPortalsToChaman.SetActive(true);
        await Task.Delay(1000);
        meteorites.SetActive(false);
        await Task.Delay(6000);
        PlayMusic();
        MainMenu.Instance.shoot.SetActive(true);
        await Task.Delay(4000);
        MainCamera.instance.cameraChamanToTowerLeft.SetActive(true);

        LoadEnemies();

    }

    public async Task ActivateAnimationPhaseTwo()
    {
        MainCamera.instance.cameraFrontRunner.SetActive(true);
        await Task.Delay(8000);
        meteorites.SetActive(true);
        await Task.Delay(2000);
        MainCamera.instance.camera3PersonRunner.SetActive(true);
        await Task.Delay(6000);
        MainCamera.instance.cameraChamanToPortals.SetActive(true);

        portals.TurnOnRightPortal();
        await Task.Delay(6000);
        PlayMusic();
        await Task.Delay(4000);
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

            enemyManager.SendEnemies();
        }
    }
}
