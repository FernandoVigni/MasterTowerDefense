using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
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

    void Start()
    {
        SetLevelFour();
    }

    public void EndLevel() 
    {
       // if(enemyManager.lis)    
    }

    public float GetCurrentLevel() 
    {
        return coefficient;
    }

    public void SetLevelOne()
    {
        coefficient = 1;
        enemyManager.SetCurrentCoefficient(coefficient);
        ResetBasicStats();
        ammountOfWarriorsInWave = 10;
        ammountOfMagesInWave = 2;
        ammountOfGiantsInWave = 0;
        ammountOfKamikazesInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave, ammountOfKamikazesInWave);
    }


    public void SetLevelTwo()
    {
        coefficient = 1;
        ResetBasicStats();
        ammountOfWarriorsInWave = 10;
        ammountOfMagesInWave = 8;
        ammountOfGiantsInWave = 0;
        ammountOfKamikazesInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave, ammountOfKamikazesInWave);
    }

    public void SetLevelTree()
    {
        coefficient = 1.2f;
        ResetBasicStats();
        ammountOfWarriorsInWave = 4;
        ammountOfMagesInWave = 15;
        ammountOfGiantsInWave = 0;
        ammountOfKamikazesInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave, ammountOfKamikazesInWave);
    }


    public void SetLevelFour()
    {
        coefficient = 1.5f;
        ResetBasicStats();
        ammountOfWarriorsInWave = 2;
        ammountOfMagesInWave = 12;
        ammountOfGiantsInWave = 1;
        ammountOfKamikazesInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave, ammountOfKamikazesInWave);
    }

    public void SetLevelfive()
    {
        coefficient = 5;
        ResetBasicStats();
        ammountOfWarriorsInWave = 8;
        ammountOfMagesInWave = 8;
        ammountOfGiantsInWave = 2;
        ammountOfKamikazesInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave, ammountOfKamikazesInWave);
    }



    void ResetBasicStats()
    {
        enemyManager.RemoveAllInStage();
        enemyManager.RemoveAllInCollider();
    }

    public void LoadEnemies(int ammountOfWarriorsInWave,int ammountOfMagesInWave, int ammountOfGiantsInWave, int ammountOfKamikazesInWave)
    {
        for (int i = 0; i < ammountOfWarriorsInWave; i++)
        {
            enemyManager.InstantiateWarrior();
        }

        for (int i = 0; i < ammountOfMagesInWave; i++)
        {
            enemyManager.InstantiateMage();
        }
       
        for (int i = 0; i < ammountOfGiantsInWave; i++)
        {
            enemyManager.InstantiateGiant();
        }

        for (int i = 0; i < ammountOfKamikazesInWave; i++)
        {
            enemyManager.InstantiateKamikaze();
        }

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
