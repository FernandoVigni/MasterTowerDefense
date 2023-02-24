using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public int level;
    public int wave;
    public float waveLimitTime;
    public int ammountOfWarriorsInWave;
    public int ammountOfMagesInWave;
    public int ammountOfGiantsInWave;

    // Start is called before the first frame update
    void Start()
    {
        StartLevelOne();
    }

    void ResetBasicStats()
    {
        enemyManager.listOfEnemiesToDefeatInThisStage.RemoveAll(Enemy => true);
        enemyManager.listOfEnemiesInsideTheTowerCollider.RemoveAll(Enemy => true);
    }

    public void StartLevelOne()
    {
        ResetBasicStats();
        ammountOfWarriorsInWave = 2;
        ammountOfMagesInWave = 2;
        ammountOfGiantsInWave = 0;
        StartLevel(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave);
    }

    public void StartLevel(int ammountOfWarriorsInWave,int ammountOfMagesInWave, int ammountOfGigantsInWave)
    {
        for (int i = 0; i < ammountOfWarriorsInWave; i++)
        {
            enemyManager.InstantiateWarrior();
        }

        for (int i = 0; i < ammountOfMagesInWave; i++)
        {
            enemyManager.InstantiateMage();
        }
/*
        for (int i = 0; i < ammountOfGigantsInWave; i++)
        {
            InstantitateGiant();
        }*/
    }
}
