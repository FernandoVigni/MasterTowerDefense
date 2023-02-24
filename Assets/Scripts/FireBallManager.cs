using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : Projectile
{
    public FireBall fireBallPrefab;
    public  FireBall firstFireBall;
    public FireBall fireBall;
    public GameObject spawnPoint;
    public Transform PointToStartShoot;
    public int numFireBalls;
    public  Vector3 positionToInstantiate;
    public List<FireBall> fireBalls = new List<FireBall>();

    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            positionToInstantiate = PointToStartShoot.position;
            FireBall fireBall = Instantiate(fireBallPrefab, positionToInstantiate,  Quaternion.identity);
            fireBall.translate = false;
            fireBalls.Add(fireBall);
        }
    }
   
    public void ShootFireball(Enemy enemy)
    {
        fireBall = ChooseFirstFireBall();
        fireBall.transform.position = PointToStartShoot.position;
        fireBall.LookAt(enemy);
        fireBall.StartMove();
    }

    public FireBall ChooseFirstFireBall()
    {
        if (fireBalls.Count <= 0)
        {
            return null;
        }
        firstFireBall = fireBalls[0];
        MoveFirstFireBallToTheEndOfList(firstFireBall);
        return firstFireBall;
    }

    public void MoveFirstFireBallToTheEndOfList(FireBall fireBall)
    {
        fireBalls.RemoveAt(0);
        fireBalls.Add(fireBall);
    }
}
