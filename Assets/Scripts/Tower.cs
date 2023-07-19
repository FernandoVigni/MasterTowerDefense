using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{ 
    public FireBallManager fireBallManager;
    public EnemyManager enemyManager;
    public Enemy objetive;
    public ExplosiveMine explosiveMinePrefabA;
    public ExplosiveMine explosiveMinePrefabB;
    public float countDownToShoot;
    public float countDownReset;
    public float manualShoot;
    private float life;
    public float gold;
    public float launchForce;
    public int ammountOfMines;
    public int secondsBetweenMines;

    public Transform CornerA;
    public Transform CornerB;
    public Transform CornerC; 
    public Transform CornerD;

    private void Start()
    {
        ThrowExplosiveMines(ammountOfMines, secondsBetweenMines);
    }

    void Update()
    {
        countDownToShoot = countDownToShoot - Time.deltaTime;
        if (countDownToShoot < 0 && enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() > 0)
        {
            Debug.Log("Disparando");
            ShootNearestEnemy();
        }
    }

    public void ShootNearestEnemy()
    {
        if(countDownToShoot < manualShoot) 
        { 
            Enemy nearestEnemyInsideCollider = GetNearestEnemyInsideCollider();
            countDownToShoot = countDownReset;
            if (nearestEnemyInsideCollider != null);
            Shoot(nearestEnemyInsideCollider);
        }
    }

    public Enemy GetNearestEnemyInsideCollider()
    {
        if (enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() > 1)
        {
            enemyManager.SortlistOfEnemiesInsideTheTowerCollider();
            return enemyManager.GetIEnemyFromColliderList(0);
        }
        if (enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() == 1)
        {
            Enemy IEnemy = enemyManager.GetIEnemyFromColliderList(0);
            return IEnemy;
        }
        else
        return null;
    }

    public Vector3 GetTowerPosition() 
    {
        return transform.position;
    }

    public void Shoot(Enemy objetive)
    {
        if (objetive != null)
        {
            fireBallManager.ShootProjectile(objetive);
        }
    }

    public void ReciveDamage(float damage)
    {
        life -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemyManager.IsCointaind(enemy)) 
            {
                enemyManager.AddEnemyInsideColliderlist(enemy);
            }
        }            
    }


    // Método para lanzar repetidamente un objeto explosivo y realizar una explosión al colisionar
    public void ThrowExplosiveMines(int count, float delayBetweenThrows)
    {
        StartCoroutine(ThrowMinesCoroutine(count, delayBetweenThrows));
    }

    private IEnumerator ThrowMinesCoroutine(int count, float delayBetweenThrows)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delayBetweenThrows);
            Vector3 launchPosition = GetRandomPointBetweenCorners();
            ExplosiveMine explosiveMine = Instantiate(explosiveMinePrefabB, launchPosition, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPointBetweenCorners()
    {
        Vector3 cornerAPosition = CornerA.position;
        Vector3 cornerBPosition = CornerB.position;
        Vector3 cornerCPosition = CornerC.position;
        Vector3 cornerDPosition = CornerD.position;

        float minX = Mathf.Min(cornerAPosition.x, cornerBPosition.x, cornerCPosition.x, cornerDPosition.x);
        float maxX = Mathf.Max(cornerAPosition.x, cornerBPosition.x, cornerCPosition.x, cornerDPosition.x);

        float minY = Mathf.Min(cornerAPosition.y, cornerBPosition.y, cornerCPosition.y, cornerDPosition.y);
        float maxY = Mathf.Max(cornerAPosition.y, cornerBPosition.y, cornerCPosition.y, cornerDPosition.y);

        float minZ = Mathf.Min(cornerAPosition.z, cornerBPosition.z, cornerCPosition.z, cornerDPosition.z);
        float maxZ = Mathf.Max(cornerAPosition.z, cornerBPosition.z, cornerCPosition.z, cornerDPosition.z);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, randomY, randomZ);
    }
}
