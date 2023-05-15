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
    public EnemyManager enemyManager;
    public Transform pointOfSpawnOfWave;

    //----------
    int[] coefficient = { 1, 1, 1, 1 };
    int[] amountOfBagOfGoldByPhase = { 1, 2, 5, 5 };
    int[] amountOfWarriosByPhase = { 30, 10, 15, 15 };
    int[] amountOfMagesByPhase = { 10, 15, 15, 15 };
    int[] amountOfRunnersByPhase = { 0, 30, 0, 0 };
    int[] amountOfGiantsByPhase = { 3, 3, 6, 6 };
    //----------

    public int currentPhase= 0;
    public float waveLimitTime;
    public float bagOfGold;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public int GetCoefficient() 
    {
        return coefficient[currentPhase];
    }

    private void Start()
    {
        StartPhase();
    }

    public void SetPhasePlusOne()
    {
        currentPhase += 1;
    }

    public void StartPhase() 
    {
        SetBagOfGold();
        LoadEnemies();
    }

    public bool nextPhase() 
    {
        if (currentPhase <= amountOfBagOfGoldByPhase.Length)
            { return true ; }
        else
            { return false; }
    }

    public int GetAmountOfPhases() 
    {
        int amountOfPhases = amountOfWarriosByPhase.Length;
        return amountOfPhases;
    }
    public void SetBagOfGold()
    {
        bagOfGold = amountOfBagOfGoldByPhase[currentPhase];
    }



    public float GetAmountBagOfGold() 
    {
        return bagOfGold;
    }

    public void LoadEnemies()
    {
        for (int i = 0; i < amountOfWarriosByPhase[currentPhase]; i++)
            {enemyManager.InstantiateWarrior();}

        for (int i = 0; i < amountOfMagesByPhase[currentPhase]; i++)
            {enemyManager.InstantiateMage();}
       
        for (int i = 0; i < amountOfGiantsByPhase[currentPhase]; i++)
            {enemyManager.InstantiateGiant();}

        for (int i = 0; i < amountOfRunnersByPhase[currentPhase]; i++)
            {enemyManager.InstantiateRunner();}

        enemyManager.SendEnemies();
    }
}
