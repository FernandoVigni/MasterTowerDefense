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
    private int life;

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0 && enemyManager.listOfEnemiesInsideTheTowerCollider.Count > 0)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            countDown = countDownReset;
            Shoot(nearestEnemy);        
        }
    }

    public Enemy GetNearestEnemy()
    {
        if (enemyManager.listOfEnemiesInsideTheTowerCollider.Count > 1)
        {
            enemyManager.listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
            return enemyManager.listOfEnemiesInsideTheTowerCollider[0];
        }
        if (enemyManager.listOfEnemiesInsideTheTowerCollider.Count == 1)
            return enemyManager.listOfEnemiesInsideTheTowerCollider[0];

        return null;
    }

    public void Shoot(Enemy objetive) 
    {
         fireBallManager.ShootProjectile(objetive);
    }

    public void ReciveDamage(int damage)
    {
        life -= damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemyManager.listOfEnemiesInsideTheTowerCollider.Contains(enemy)) 
            {
                enemyManager.listOfEnemiesInsideTheTowerCollider.Add(enemy);
            }
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemyManager.listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            enemyManager.listOfEnemiesInsideTheTowerCollider.Remove(enemy);
        }
    }
}
