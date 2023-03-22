using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : Projectile
{
    public FireBall fireBallPrefab;
    public FireBall firstFireBall;
    public FireBall fireBall;
    public GameObject spawnPoint;
    public int numFireBalls;
    public  Transform positionToInstantiateProjectil;
    public List<FireBall> fireBalls = new List<FireBall>();

    void Start()
    {
        for (int i = 0; i < numFireBalls; i++)
        {
            FireBall fireBall = Instantiate(fireBallPrefab, positionToInstantiateProjectil.position,  Quaternion.identity);
            fireBall.translate = false;
            fireBalls.Add(fireBall);
        }
    }
   
    public void ShootProjectile(Enemy enemy)
    {
        fireBall = ChooseFirstProjectile();
        fireBall.transform.position = positionToInstantiateProjectil.position;
        fireBall.LookAt(enemy);
        fireBall.StartMove();
    }

    public FireBall ChooseFirstProjectile()
    {
        if (fireBalls.Count <= 0)
        {
            Debug.Log("null");
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

    public void MoveToInitialZone(Projectile projectile)
    {
        projectile.transform.rotation = Quaternion.identity;
        projectile.transform.position = positionToInstantiateProjectil.position;
    }
}
