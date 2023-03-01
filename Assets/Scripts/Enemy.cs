using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnDeath;
    public EnemyManager enemyManager;
    public Tower tower;
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
        float distance = Vector3.Distance(transform.position, tower.GetTowerPosition());
        return distance;
    }

    private void Walk()
    {
        if (distanceToTower > 2)
        {
            transform.LookAt(tower.GetTowerPosition());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void StartMove()
    {
        isWalking = true;
    }

    public void StopMove()
    {
        isWalking = false;
    }

    private bool IsAlive() 
    {
        return life > 0;
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
        {
            OnDeath(this);
        }
    }
}
