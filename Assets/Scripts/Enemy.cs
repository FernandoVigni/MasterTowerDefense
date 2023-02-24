using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Tower tower;
    public Action<Enemy> OnDeath;
    public EnemyManager enemyManager;
    public int physicalDamage;
    public int magicDamage;
    public int life;
    public float speed;
    public int armor;
    public bool isWalking;
    public float distanceToTower;

   // private IPromise deathPromes;

    private void Update()
    {
        if(isWalking)
            Walk();
    }

    public void ReceibeDamage(int damage) 
    {
        life -= damage;
        if (!IsAlive()) 
            OnDeath?.Invoke(this);
    }

    private bool IsAlive() 
    {
        return life > 0;
    }

    private void Walk()
    {
        if (tower != null && distanceToTower > 2 )
        {
            transform.LookAt(tower.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
