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
    private MainMenu canva;
    private AudioManager audioManager;
    public PortalsManager portals;

    //----------
    int[] coefficient = { 1, 1, 1, 2 };
    int[] amountOfBagOfGoldByPhase = { 1, 2, 5, 5 };
    int[] amountOfWarriosByPhase = { 30, 10, 15, 15, };
    int[] amountOfMagesByPhase = { 10, 20, 15, 15 };
    int[] amountOfRunnersByPhase = { 0, 30, 0, 0 };
    int[] amountOfGiantsByPhase = { 3, 3, 6, 6 };
    int[] amountOfBosses = { 0, 0, 0, 1 };
    string[] songsNames = { "MainMenu", "Phase0", "Phase1", "Phase1" };   
    //----------

    public int currentPhase;
    public float waveLimitTime;

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
        canva = FindObjectOfType<MainMenu>();
        audioManager = FindAnyObjectByType<AudioManager>(); 
        currentPhase = 0;
    }

    public void TurnOnPortals() 
    {
        portals.TurnOnLeftPortal();
        portals.TurnOnRightPortal();
    }

    public void TurnOffPortals() 
    {
        portals.TurnOffLeftPortal();
        portals.TurnOffRightPortal();
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

    public void StartPhase() 
    {
        portals.TurnOnLeftPortal();
        portals.TurnOnRightPortal();
        LoadEnemies();
        SetPhasePlusOne();
        string name = songsNames[currentPhase];
        audioManager.PlayMusic(name);
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
