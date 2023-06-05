using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{ 
    public FireBallManager fireBallManager;
    public EnemyManager enemyManager;
    public Enemy objetive;
    public float countDown;
    public float countDownReset;
    private float life;
    public float gold;

    void Update()
    {
        countDown = countDown - Time.deltaTime;

        if (countDown < 0 && enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() > 0)
        {
            Enemy nearestEnemyInsideCollider = GetNearestEnemyInsideCollider();
            countDown = countDownReset;
            if (nearestEnemyInsideCollider != null);
             Shoot(nearestEnemyInsideCollider);        
        }
    }

    public void RecibeGold(float gold)
    {
        PhaseManager.Instance.coinCount += gold;
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
         fireBallManager.ShootProjectile(objetive);
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
}
