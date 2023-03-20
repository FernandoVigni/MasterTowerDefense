using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public EnemyManager enemyManager;
    public Transform PointOfSpawnOfWave;

    public int level;
    public int wave;
    public float waveLimitTime;
    public float coefficient = 2;
    public float bagOfGold;

    public int ammountOfWarriorsInWave;
    public int ammountOfMagesInWave;
    public int ammountOfGiantsInWave;
    public int ammountOfKamikazesInWave;

    void Start()
    {
        SetLevelOne();
    }

    public float GetAmountBagOfGold() 
    {
        return bagOfGold;
    }

    public void SetBagOfGold(float bagOfGold) 
    {
        this.bagOfGold = bagOfGold;
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
        SetBagOfGold(2000f);
        SetEnemies(1, 0, 0, 0);
        LoadEnemies();
    }

    public void SetLevelTwo()
    {
        coefficient = 1;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetBagOfGold(1000f);
        SetEnemies(10, 8, 0, 0);
        LoadEnemies();
    }

    public void SetLevelTree()
    {
        coefficient = 1.2f;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetBagOfGold(1500f);
        SetEnemies(2, 10, 0, 0);
        LoadEnemies();
    }

    public void SetLevelFour()
    {
        coefficient = 1.5f;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetBagOfGold(2000f);
        SetEnemies(2, 12, 1, 0);
        LoadEnemies();
    }

    public void SetLevelfive()
    {
        coefficient = 5;
        enemyManager.SetCurrentCoefficient(coefficient);
        SetBagOfGold(2500f);
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
