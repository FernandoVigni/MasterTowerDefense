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
    public int ammountOfWarriorsInWave;
    public int ammountOfMagesInWave;
    public int ammountOfGiantsInWave;
    public int ammountOfKamikazesInWave;

    void Start()
    {
        PreSetLevelOne();
    }

    //Todo Reveer el nombre de las variables
    public void PreSetLevelOne()
    {
        ResetBasicStats();
        ammountOfWarriorsInWave = 15;
        ammountOfMagesInWave = 15;
        ammountOfGiantsInWave = 8;
        ammountOfKamikazesInWave = 22;
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
          /*  Type warriorType = typeof(Warrior);
            Enemy newWarrior = (Enemy)Activator.CreateInstance(warriorType);
            enemyManager.EnemySet(newWarrior);*/
            enemyManager.InstantiateWarrior();
        }

        for (int i = 0; i < ammountOfMagesInWave; i++)
        {
            // enemyManager.InstantiateEnemy(typeof(Mage));
            enemyManager.InstantiateMage();
        }
       
        for (int i = 0; i < ammountOfGiantsInWave; i++)
        {
            // enemyManager.InstantiateEnemy(typeof(Giant));
            enemyManager.InstantiateGiant();
        }

        for (int i = 0; i < ammountOfKamikazesInWave; i++)
        {
            // enemyManager.InstantiateEnemy(typeof(Giant));
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
                enemy.transform.position = PointOfSpawnOfWave.transform.position;
                enemy.LookAt();
                enemy.StartMove();
                enemyManager.RemoveEnemyFromStageList(enemy);
                enemyManager.AddEnemyToSentList(enemy);
            } 
        }   
    }       
}
