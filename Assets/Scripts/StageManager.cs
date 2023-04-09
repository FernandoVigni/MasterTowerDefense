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

    float[] coefficientsByLevel = { 1f, 1.2f, 1.5f, 1.75f, 2f, 2.5f };
    int[] amountOfBagOfGoldByLevel = { 1000, 1200, 1300, 2000, 2500 };    
    int[] amountOfWarriosByLevel = { 10, 10, 15, 20, 5 };
    int[] amountOfGiantsByLevel = {4, 7, 6, 8, 3 };
    int[] amountOfMagesByLevel = { 8, 8, 2, 5, 5 };
    int[] amountOfKamikazesByLevel = { 7, 0, 0, 0, 0 };

    public int currentLevel;
    public int wave;
    public float waveLimitTime;
    public float bagOfGold;
    public float spawnDistanceToTower;

    private void Start()
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

                // 1) random 0 - 360      90
                int randonAngle = GetRandomNumber(30, 140);

                // 2) calculo sin         
                double x = GetSineOfAnAngle(randonAngle);
                float floatX = (float)x;

                // 3) calculo cos         
                double y = GetCosOfAnAngle(randonAngle);
                float floatY = (float)y;

                Vector3 spawnPositionRandom = new Vector3(floatX, 0, floatY);
                enemy.transform.position = spawnPositionRandom;
                enemy.LookTower();
                enemy.StartMove();
                enemyManager.RemoveEnemyFromStageList(enemy);
                enemyManager.AddEnemyToSentList(enemy);
            } 
        }   
    }

    public double GetSineOfAnAngle(int angle) 
    {
        double angleInRadians = angle * Math.PI / 180;
        double sineValue = Math.Sin(angleInRadians);
        sineValue = sineValue * spawnDistanceToTower;
        sineValue = Math.Round(sineValue, 2);
        return sineValue;
    }

    public double GetCosOfAnAngle(int angle)
    {
        double angleInRadians = angle * Math.PI / 180;
        double cosValue = Math.Cos(angleInRadians);
        cosValue = cosValue * spawnDistanceToTower;
        cosValue = Math.Round(cosValue, 2);
        return cosValue;
    }

    public int GetRandomNumber(int min, int max)
    {
        int randomNumber = UnityEngine.Random.Range(min, max);
        return randomNumber;
    }
}
