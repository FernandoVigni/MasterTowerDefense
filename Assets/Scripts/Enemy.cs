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
    public int magicArmor;
    public int incomeValueOnDeath;
    public bool isWalking;
    public float speed;
    public float distanceToTower;
    public int currentLevel;

    void Update()
    {
        if (isWalking)
        {
            distanceToTower = CalculateDistanceToTower();
            Walk();
        }
    }
    
    public void SetCurrentLevel(int currentLevel) 
    {
        this.currentLevel = currentLevel;
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
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void LookAt()
    {
        transform.LookAt(tower.GetTowerPosition());
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
        this.life = life * currentLevel;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed * currentLevel;
    }

    public void SetMagicArmor(int magicArmorValue) 
    {
        magicArmor = magicArmorValue * currentLevel;
    }

    public void SetIncomeValue(int value)
    {
        incomeValueOnDeath = value * currentLevel;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
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
