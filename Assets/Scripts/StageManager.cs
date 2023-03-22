using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Transform pointOfSpawnOfWave;

    int[] amountOfBagOfGoldByLevel = { 1000, 1200, 1300, 2000, 2500 };
    float[] coefficientsByLevel = { 1f, 1.2f, 1.5f, 1.75f, 2f, 2.5f };
    int[] amountOfWarriosByLevel = { 4, 10, 15, 20, 5 };
    int[] amountOfGiantsByLevel = { 4, 7, 6, 8, 3 };
    int[] amountOfMagesByLevel = { 4, 8, 2, 5, 5 };
    int[] amountOfKamikazesByLevel = { 4, 0, 0, 0, 0 };

    public int level;
    public int wave;
    public float waveLimitTime;
    public float bagOfGold;

    void Start()
    {
        SetLevel(0);
    }

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public void SetLevel(int curentLevel)
    {
        SetBagOfGold(amountOfBagOfGoldByLevel[curentLevel]);
        enemyManager.SetCurrentCoefficient(coefficientsByLevel[curentLevel]);
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
        for (int i = 0; i < amountOfWarriosByLevel[currentLevel]; i++)
            {enemyManager.InstantiateWarrior();}

        for (int i = 0; i < amountOfMagesByLevel[currentLevel]; i++)
            {enemyManager.InstantiateMage();}
       
        for (int i = 0; i < amountOfGiantsByLevel[currentLevel]; i++)
            {enemyManager.InstantiateGiant();}

        for (int i = 0; i < amountOfKamikazesByLevel[currentLevel]; i++)
            {enemyManager.InstantiateKamikaze();}

        SendEnemies();
    }

    public  async void SendEnemies()
    {
        int enemiesInThisLevel = enemyManager.GetAmmountOflistOfEnemiesToDefeatInThisStage();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemiesInThisLevel > 0);
            {
                enemyManager.ShufleList(enemyManager.listOfEnemiesToDefeatInThisStage);
                Enemy enemy = enemyManager.GetFirstEnemyFromStageList();
                await Task.Delay(1500);
              //  enemy.recalculateWithTheCoefficientOfTheLevel(coefficient);
                enemy.transform.position = pointOfSpawnOfWave.transform.position;
                enemy.LookTower();
                enemy.StartMove();
                enemyManager.RemoveEnemyFromStageList(enemy);
                enemyManager.AddEnemyToSentList(enemy);
            } 
        }   
    }       
}
