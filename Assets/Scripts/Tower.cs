using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    List<Enemy> enemiesInsideCollider = new List<Enemy>();
    public FireBallManager fireBallManager;
    public EnemyManager enemyManager;
    public float countDown;
    public float countDownReset;
    public Enemy objetive;
    private int life;

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        
        if(countDown <= 0) 
        {
            Enemy nearestEnemy = enemyManager.GetNearestEnemyToTower();

        }

        /*
        if(enemyManager.listOfEnemiesInsideTheTowerCollider != null && enemyManager.ammountOfEnemiesInsideTowerCollider > 0  && countDown <= 0)
        {
            countDown = countDownReset;
            objetive = enemyManager.GetNearestEnemyPosition();
            fireBallManager.ShootNewFireball(objetive);   
        }*/
    }

    public List<Enemy> GetEnemiesInsideCollider()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemyManager.listOfEnemiesInsideTheTowerCollider.Add(enemy);
            }
        }
        return enemyManager.listOfEnemiesInsideTheTowerCollider;
    }

    public void Shoot(Enemy objetive) 
    {
        enemiesInsideCollider = GetEnemiesInsideCollider();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.listOfEnemiesInsideTheTowerCollider.Add(enemy);
        }
    }

    public void ReciveDamage(int damage) 
    {
        life -= damage;   
    }
}
