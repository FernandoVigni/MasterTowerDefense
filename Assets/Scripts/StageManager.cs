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

    void Start()
    {
        PreSetLevelOne();
    }

    //Todo Reveer el nombre de las variables
    public void PreSetLevelOne()
    {
        ResetBasicStats();
        ammountOfWarriorsInWave = 0;
        ammountOfMagesInWave = 1;
        ammountOfGiantsInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave);
    }

    void ResetBasicStats()
    {
        enemyManager.RemoveAllInStage();
        enemyManager.RemoveAllInCollider();
    }

    public void LoadEnemies(int ammountOfWarriorsInWave,int ammountOfMagesInWave, int ammountOfGigantsInWave)
    {
        for (int i = 0; i < ammountOfWarriorsInWave; i++)
        {
            enemyManager.InstantiateWarrior();
        }

        for (int i = 0; i < ammountOfMagesInWave; i++)
        {
            enemyManager.InstantiateMage();
        }
       
        for (int i = 0; i < ammountOfGigantsInWave; i++)
        {
            enemyManager.InstantiateGiant();
        }
        SendEnemies();
    }

    public  async void SendEnemies()
    {
        int enemiesInThisLevel = enemyManager.listOfEnemiesToDefeatInThisStage.Count;
     //   enemyManager.ShufleList(enemyManager.listOfEnemiesToDefeatInThisStage);
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemyManager.GetAmmountOflistOfEnemiesToDefeatInThisStage() >= 1);

            { 
                Enemy enemy = enemyManager.GetIEnemyFromStageList(i);
                await Task.Delay(3500);
                enemy.transform.position = PointOfSpawnOfWave.transform.position;
                enemy.StartMove();
            } 
        }   
    }   // hay que dividir la cantidad de enemigos en el tiempo y de ahi el tiempo entre spawn    
}
