using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Transform pointOfSpawnOfWave;

    float[] coefficientsByPhase = { 1f, 1f, 1f, 1f };
    int[] amountOfBagOfGoldByPhase = { 1, 2, 5 , 5};    
    int[] amountOfWarriosByPhase = { 30, 10, 15, 15 };
    int[] amountOfMagesByPhase = { 10, 15, 15, 15 };
    int[] amountOfRunnersByPhase = { 0, 30, 0, 0 };
    int[] amountOfGiantsByPhase = { 3 ,3 , 6, 6 };
    
    public int currentLevel;
    public int wave;
    public float waveLimitTime;
    public float bagOfGold;


    private void Start()
    {
        SetCoefficientAndBagOfGold(1);
    }

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public void SetCoefficientAndBagOfGold(int curentLevel)
    {
        SetBagOfGold(amountOfBagOfGoldByPhase[curentLevel]);
        enemyManager.SetCurrentCoefficient(coefficientsByPhase[curentLevel]);
        LoadEnemies(curentLevel);
    }

    public float GetAmountBagOfGold() 
    {
        return bagOfGold;
    }

    public void SetBagOfGold(float bagOfGold) 
    {
        this.bagOfGold = bagOfGold;
    }

    public void LoadEnemies(int currentLevel)
    {
        for (int i = 0; i < amountOfWarriosByPhase[currentLevel]; i++)
            {enemyManager.InstantiateWarrior();}

        for (int i = 0; i < amountOfMagesByPhase[currentLevel]; i++)
            {enemyManager.InstantiateMage();}
       
        for (int i = 0; i < amountOfGiantsByPhase[currentLevel]; i++)
            {enemyManager.InstantiateGiant();}

        for (int i = 0; i < amountOfRunnersByPhase[currentLevel]; i++)
            {enemyManager.InstantiateRunner();}

        enemyManager.SendEnemies();
    }
}
