using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public IncomeManager IncomeManager;
    public EnemyManager enemyManager;
    public Transform PointOfSpawnOfWave;
    public int level;
    public int wave;
    public float waveLimitTime;
    public float coefficient;
    public int ammountOfWarriorsInWave;
    public int ammountOfMagesInWave;
    public int ammountOfGiantsInWave;
    public int ammountOfKamikazesInWave;
    public float bagOfGold = 1000;

    void Start()
    {
        SetLevelOne();
    }

    public void EndLevel() 
    {
        Debug.Log("Termino el nivel");
        IncomeManager.ReciveBagOfGold(bagOfGold);
    }

    public void SetEnemies(int warrios, int mages, int giants, int kamikazes)
    {
        ammountOfWarriorsInWave = warrios;
        ammountOfMagesInWave = mages;
        ammountOfGiantsInWave = giants;
        ammountOfKamikazesInWave = kamikazes;
    }

    public void SetLevelOne()
    {
        coefficient = 1.25f;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetEnemies(10, 2, 0, 0);
        LoadEnemies();
    }

    public void SetLevelTwo()
    {
        coefficient = 1;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetEnemies(10, 8, 0, 0);
        LoadEnemies();
    }



    public void SetLevelTree()
    {
        coefficient = 1.2f;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetEnemies(2, 10, 0, 0);
        LoadEnemies();
    }

    public void SetLevelFour()
    {
        coefficient = 1.5f;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetEnemies(2, 12, 1, 0);
        LoadEnemies();
    }

    public void SetLevelfive()
    {
        coefficient = 5;
        SetEnemies(8, 8, 2, 0);
        LoadEnemies();
    }

    public void LoadEnemies()
    {
        for (int i = 0; i < ammountOfWarriorsInWave; i++)
            {enemyManager.InstantiateWarrior();}

        for (int i = 0; i < ammountOfMagesInWave; i++)
            {enemyManager.InstantiateMage();}
       
        for (int i = 0; i < ammountOfGiantsInWave; i++)
            {enemyManager.InstantiateGiant();}

        for (int i = 0; i < ammountOfKamikazesInWave; i++)
            {enemyManager.InstantiateKamikaze();}

        SendEnemies();
    }

    public  async void SendEnemies()
    {
        int enemiesInThisLevel = enemyManager.GetAmmountOflistOfEnemiesToDefeatInThisStage();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemyManager.GetAmmountOflistOfEnemiesToDefeatInThisStage() >= 1);
            {
                enemyManager.ShufleList(enemyManager.listOfEnemiesToDefeatInThisStage);
                Enemy enemy = enemyManager.GetFirstEnemyFromStageList();
                await Task.Delay(1500);
                enemy.recalculateWithTheCoefficientOfTheLevel(coefficient);
                enemy.transform.position = PointOfSpawnOfWave.transform.position;
                enemy.LookAt();
                enemy.StartMove();
                enemyManager.RemoveEnemyFromStageList(enemy);
                enemyManager.AddEnemyToSentList(enemy);
            } 
        }   
    }       
}
