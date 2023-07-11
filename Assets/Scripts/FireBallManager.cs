using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public FireBall fireBallPrefab;
    public GameObject spawnPoint;
    public int numFireBalls;
    public Transform positionToMoveProjectil;
    public List<FireBall> fireBalls = new List<FireBall>();
    private int currentFireBallIndex = 0;

    void Start()
    {
        for (int i = 0; i < numFireBalls; i++)
        {
            FireBall fireBall = Instantiate(fireBallPrefab, positionToMoveProjectil.position, Quaternion.identity);
            fireBall.translate = false;
            fireBall.gameObject.SetActive(false); // Desactivar la fireball al instanciarla
            fireBalls.Add(fireBall);
        }
    }

    public void ShootProjectile(Enemy enemy)
    {
        FireBall fireBall = ChooseNextProjectile();
        fireBall.transform.position = positionToMoveProjectil.position;
        fireBall.LookAt(enemy);
        fireBall.gameObject.SetActive(true); // Activar la fireball antes de dispararla
        fireBall.StartMove();
    }

    public FireBall ChooseNextProjectile()
    {
        if (fireBalls.Count <= 0)
        {
            Debug.Log("No hay más fireballs disponibles.");
            return null;
        }

        FireBall nextFireBall = fireBalls[currentFireBallIndex];
        currentFireBallIndex++;
        if (currentFireBallIndex >= fireBalls.Count)
        {
            currentFireBallIndex = 0;
        }
        return nextFireBall;
    }

    public async Task MoveToInitialZone(Projectile projectile)
    {
        await Task.Delay(350);
        projectile.transform.position = positionToMoveProjectil.position;
        projectile.gameObject.SetActive(false); // Desactivar la fireball al moverla a la zona inicial
    }

    public void RemoveAllFireballs()
    {
        foreach (FireBall fireBall in fireBalls)
        {
            Destroy(fireBall.gameObject);
        }
        fireBalls.Clear();
    }
}