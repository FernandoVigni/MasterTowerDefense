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

        if (countDown <= 0 && enemyManager.listOfEnemiesToDefeatInThisStage.Count > 0)
        {
            GetEnemiesInsideCollider();
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null)
            {
                countDown = countDownReset;
                Shoot(nearestEnemy);
            }
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
         fireBallManager.ShootFireball(objetive);
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
