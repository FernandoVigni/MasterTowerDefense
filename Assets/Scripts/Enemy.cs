using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public bool OnDeathWasExecuted = false;
    public Action<Enemy> OnDeath;
    public Tower tower;
    private Animator animator;

    public float life;
    public float speed;
    public float physicalDamage;
    public float physicalArmor;
    public float magicDamage;
    public float magicArmor;
    public int goldValueOnDeath;
    public float atackSpeed;

    public bool isWalking;
    public float distanceToTower;
    public float coefficient;

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
      //  animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWalking)
        {
            distanceToTower = CalculateDistanceToTower();
            if (distanceToTower > 250)
                { DestroyEnemy(); }

            Walk();
        }
    }

    public void RunAnimation()
    {
        //animator.SetBool("Run", true);
    }

    public void recalculateStatsWithTheCoefficient(float coefficient) 
    {
        goldValueOnDeath = (int)((float)goldValueOnDeath * coefficient);
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
        if (distanceToTower > 55 && GetComponent<Mage>() == null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (distanceToTower > 80 && GetComponent<Mage>() != null) 
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

    public void SetCoefficient(float coefficient) 
    {
        this.coefficient = coefficient;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    //EJEMPLO POLIMORFISMO
    public virtual void BossScream() { }

    public int recivedGoldInThisPhase; 
    public void ReceibeDamage(int damage)
    {
        life -= damage;
        if (!IsAlive() && !OnDeathWasExecuted)
        {
            recivedGoldInThisPhase += goldValueOnDeath;
            OnDeathWasExecuted = true;
            OnDeath.Invoke(this);
        }
    }
}
