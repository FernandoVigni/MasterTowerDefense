using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
    }

    public bool OnDeathWasExecuted = false;
    public Action<Enemy> OnDeath;
    public Tower tower;

    public float physicalDamage;
    public float magicDamage;
    public float life;
    public float magicArmor;
    public float goldValueOnDeath;
    public bool isWalking;
    public float speed;
    public float distanceToTower;
    public float coefficient;

    void Update()
    {
        if (isWalking)
        {
            distanceToTower = CalculateDistanceToTower();
            Walk();
        }
    }
    
    public void recalculateWithTheCoefficient(float coefficient) 
    {
        goldValueOnDeath *= coefficient;
        life *= coefficient;
        speed *= coefficient;
        physicalDamage *= coefficient;
        magicDamage *= coefficient;
        magicArmor *= coefficient;
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

    public void LookTower()
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
        this.life = life;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetMagicArmor(float magicArmorValue) 
    {
        magicArmor = magicArmorValue;
    }

    public void SetMagicDamage(float magicDamage)
    {
        this.magicDamage = magicDamage;
    }

    public void SetPhysicalDamage(float physicalDamage)
    {
        this.physicalDamage = physicalDamage;
    }

    public void SetGoldValue(int value)
    {
        goldValueOnDeath = value;
    }

    public void SetCoefficient(float coefficient) 
    {
        this.coefficient = coefficient;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void ReceibeDamage(int damage)
    {
        life -= damage;
        if (!IsAlive() && !OnDeathWasExecuted)
        {
            OnDeathWasExecuted = true;
            OnDeath.Invoke(this);
        }
    }
}
