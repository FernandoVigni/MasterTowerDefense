using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public FireBallManager fireBallManager;
    public EnemyManager enemyManager;
    public float countDown;
    public float countDownReset;
    Vector3 objetive;
    private int life;

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if(enemyManager.listOfEnemiesInsideTheTowerCollider != null && enemyManager.ammountOfEnemiesInsideTowerCollider > 0  && countDown <= 0)
        {
            countDown = countDownReset;
            objetive = enemyManager.GetNearestEnemyPosition();
            fireBallManager.ShootNewFireball(objetive);   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.CheckEnemyContainedInsideList(enemy);
        }
    }

    public void RecievDamage(int damage) 
    {
        life -= damage;   
    }
}
