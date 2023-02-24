using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public FireBall fireBallPrefab;
    public  FireBall firstFireBall;
    public FireBall fireBall;
    public GameObject spawnPoint;
    public int numFireBalls;
    public float speed;
    public  Vector3 positionToInstantiate;
    public List<FireBall> fireBalls = new List<FireBall>();
    public int ammountOFireBalls;

    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            positionToInstantiate = new Vector3(0, 8f, 0f);
            FireBall fireBall = Instantiate(fireBallPrefab, positionToInstantiate,  Quaternion.identity);
            fireBall.translate = false;
            fireBalls.Add(fireBall);

            ammountOFireBalls = fireBalls.Count;
        }
    }
    
    public void GoToSpawnPointPosition(FireBall fireBall)
    {
        Vector3 vector3SpawnPoint = spawnPoint.transform.position;
        fireBall.transform.position = vector3SpawnPoint;
    }

    public void ShootNewFireball(Enemy enemy)
    {
        //GoToSpawnPointPosition(fireBall);
        fireBall = ChooseFirstFireBall();
        fireBall.LookAt(enemy.transform.position);
        fireBall.AbleToTranslate();
    }

    public FireBall ChooseFirstFireBall()
    {
        if (fireBalls.Count <= 0)
        {
            return null;
        }
        firstFireBall = fireBalls[0];
        MoveFirstFireBallToTheEnd();
        return firstFireBall;
    }

    public void MoveFirstFireBallToTheEnd()
    {
        fireBalls.RemoveAt(0);
        fireBalls.Add(firstFireBall);
    }
}
