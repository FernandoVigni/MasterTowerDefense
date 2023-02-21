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

    void start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        Debug.Log("List of Enemys inside " + enemyManager.listOfEnemiesInsideTheTowerCollider);
        Debug.Log("-----");
        Debug.Log("enemy list inside conut " + enemyManager.listOfEnemiesInsideTheTowerCollider.Count);
        Debug.Log("");
        Debug.Log("countdown "+countDown);


        if(enemyManager.listOfEnemiesInsideTheTowerCollider != null && enemyManager.listOfEnemiesInsideTheTowerCollider.Count > 1  && countDown <= 0)
        {
            countDown = countDownReset;
    
            Debug.Log("La lista es > 0");
            objetive = enemyManager.GetNearestEnemyPosition();
            Debug.Log(objetive);
            Debug.Log(fireBallManager.fireBalls.Count);
            fireBallManager.ShootNewFireball(objetive);    
                  
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    enemyManager.ActivateFirstEnemyInListInitialized();
                }
    
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemyManager.RemoveEnemyFromTheInsideList(enemy);
        }
    }
}
