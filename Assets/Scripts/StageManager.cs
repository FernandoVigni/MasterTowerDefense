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

    // Start is called before the first frame update
    void Start()
    {
        PreSetLevelOne();
    }


    public void PreSetLevelOne()
    {
        ResetBasicStats();
        ammountOfWarriorsInWave = 2;
        ammountOfMagesInWave = 2;
        ammountOfGiantsInWave = 0;
        LoadEnemies(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave);
    }

    void ResetBasicStats()
    {
        enemyManager.listOfEnemiesToDefeatInThisStage.RemoveAll(Enemy => true);
        enemyManager.listOfEnemiesInsideTheTowerCollider.RemoveAll(Enemy => true);
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


    public async void SendEnemies()
    {
        int enemiesInThisLevel = enemyManager.listOfEnemiesToDefeatInThisStage.Count;
        enemyManager.ShufleList(enemyManager.listOfEnemiesToDefeatInThisStage);
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            Enemy enemy = enemyManager.listOfEnemiesToDefeatInThisStage[i];
            enemy.transform.position = PointOfSpawnOfWave.transform.position;
            enemy.StartMove();
            await Task.Delay(2000);
        }       
    }   // hay que dividir la cantidad de enemigos en el tiempo y de ahi el tiempo entre spawn    
}
