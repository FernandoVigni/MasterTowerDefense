using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnDeath;
    public EnemyManager enemyManager;
    public int physicalDamage;
    public int magicDamage;
    public int life;
    public float speed;
    public int armor;
    public bool isWalking;
    public float distanceToTower;
 
    private void Update()
    {
        if (isWalking) 
        {
            distanceToTower = CalculateDistanceToTower();
            Walk();
        }
    }

    public float CalculateDistanceToTower() 
    {
        float distance = Vector3.Distance(transform.position, enemyManager.tower.transform.position);
        return distance;
    }

    private bool IsAlive() 
    {
        return life > 0;
    }

    private void Walk()
    {
        if (distanceToTower > 2 )
        {
            transform.LookAt(enemyManager.tower.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void SetLife(int life) 
    {
        this.life = life;
    }

    public void SetSpeed(float speed) 
    {
        this.speed = speed;
    }

    public void ReceibeDamage(int damage)
    {
        life -= damage;
        if (!IsAlive())
            OnDeath?.Invoke(this);
    }

    public void StartMove()
    {
        isWalking = true;
    }

    public void StopMove()
    {
        isWalking = false;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        if (enemyManager.listOfEnemiesToDefeatInThisStage.Contains(this))
        {
            Debug.Log("lo contenia la lista del stage");
            enemyManager.listOfEnemiesToDefeatInThisStage.Remove(this);
        }

        if (enemyManager.listOfEnemiesInsideTheTowerCollider.Contains(this))
        {

            Debug.Log("lo contenia la lista del inside collider");
            enemyManager.listOfEnemiesInsideTheTowerCollider.Remove(this);
        }
        /*
        enemyManager.listOfEnemiesToDefeatInThisStage.Remove(enemy);
        enemyManager.listOfEnemiesInsideTheTowerCollider.Remove(enemy);*/
        enemyManager.MoveEnemiesToDiscardPoint(this);
        enemy.StopMove();
        //Chequeamos que no queden mas enemigos aqui?
    }
}
