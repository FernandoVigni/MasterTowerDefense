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

        Debug.Log("desde la Tower contando "+ enemyManager.listOfEnemiesInsideTheTowerCollider.Count);

        if(enemyManager.listOfEnemiesInsideTheTowerCollider != null && enemyManager.ammountOfEnemiesInsideTowerCollider > 0  && countDown <= 0)
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
            Debug.Log(enemy);
            Debug.Log("Chequeando si esta en la lista , sino se lo agrega");
            enemyManager.CheckEnemyContainedInsideList(enemy);
        }
    }
}
