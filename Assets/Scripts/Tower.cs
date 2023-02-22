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
    public bool firstTimeCLear;

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if(enemyManager.listOfEnemiesInsideTheTowerCollider != null && enemyManager.listOfEnemiesInsideTheTowerCollider.Count > 0  && countDown <= 0)
        {
            countDown = countDownReset;
    
            Debug.Log("La lista es > 0");
            objetive = enemyManager.GetNearestEnemyPosition();
            Debug.Log(objetive);
            Debug.Log(fireBallManager.fireBalls.Count);
            fireBallManager.ShootNewFireball(objetive);    
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {

            Enemy enemy = other.GetComponent<Enemy>();
            Debug.Log("Chequeando si esta en la lista , sino se lo agrega");
            enemyManager.CheckEnemyContainedInsideList(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.RemoveEnemyFromTheInsideList(enemy);
        }
    }
}
