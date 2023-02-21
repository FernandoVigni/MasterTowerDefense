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

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(enemyManager.enemyList != null && enemyManager.enemyList.Count > 0  && countDown <= 0)
        {
            countDown = countDownReset;
            objetive = enemyManager.GetNearestEnemyPosition();
            Debug.Log(fireBallManager.fireBalls.Count);
            fireBallManager.ShootNewFireball(objetive);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.CheckEnemyContainedInList(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.RemoveEnemyFromTheList(enemy);
        }
    }
}
