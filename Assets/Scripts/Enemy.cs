using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public int randomNumberToSort;
    public bool OnDeathWasExecuted = false;
    public Action<Enemy> OnDeath;
    public Tower tower;
    private Animator animator;
    public LifeBar lifeBar;
    public EnemyManager enemyManager;
    System.Random random = new System.Random();

    public float currentLife;
    public float maxLife;
    public float speed;
    public float physicalDamage;
    public float physicalArmor;
    public float magicDamage;
    public float magicArmor;
    public int goldValueOnDeath;
    public float atackSpeed;
    public int recivedGoldInThisPhase;
    public bool isWalking;
    public bool isAtacking;
    public float distanceToTower;
    public float coefficient;
    public Vector3 scale;

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        //  animator = GetComponent<Animator>();
        randomNumberToSort = random.Next(1, 10000);
    }

    void Update()
    {
        lifeBar.SetRemainingLifeToShow(currentLife, maxLife);
        if (isWalking)
        {
            distanceToTower = CalculateDistanceToTower();
            if (distanceToTower > 350)
            {
                ReceibeDamage(1000000);
            }
            Walk();
        }
    }

    public void SetMaxLife(float maxLife)
    {
        this.maxLife = maxLife;
    }

    public void RunAnimation()
    {
        //animator.SetBool("Run", true);
    }

    public void recalculateStatsWithTheCoefficient(float coefficient)
    {
        goldValueOnDeath = (int)((float)goldValueOnDeath * coefficient);
        maxLife *= coefficient;
        currentLife = maxLife;
        speed *= coefficient;
        physicalDamage *= coefficient;
        magicDamage *= coefficient;
        magicArmor *= coefficient;

        // Incrementar las escalas X, Y, Z con el coeficiente
        if (coefficient == 2)
        {
            scale.x *= (coefficient * 0.75f);
            scale.y *= (coefficient * 0.75f);
            scale.z *= (coefficient * 0.75f);
            // Cambiar los materiales para que se vea mas fuerte
        }
    }

    public float CalculateDistanceToTower()
    {
        float distance = Vector3.Distance(transform.position, tower.GetTowerPosition());
        return distance;
    }

    private void Walk()
    {
        if (distanceToTower > 65 && (GetComponent<Giant>() != null || GetComponent<Warrior>() != null))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isAtacking = true;
        }

        if (distanceToTower < 65 && (GetComponent<Giant>() != null || GetComponent<Warrior>() != null))
        {
            isAtacking = true;
        }

        if (distanceToTower > 110 && GetComponent<Mage>() != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isAtacking = true;
        }

        if (distanceToTower < 110 && GetComponent<Mage>() != null)
        {
            isAtacking = true;
        }
    }

    public void LookTower()
    {
        if(tower != null)
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
        return currentLife > 0;
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

    public void ReceibeDamage(int damage)
    {
        currentLife -= damage;
        if (!IsAlive() && !OnDeathWasExecuted)
        {
            recivedGoldInThisPhase += goldValueOnDeath;
            OnDeathWasExecuted = true;
            OnDeath.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            if (!enemyManager.IsCointaindInListOfEnemiesInsideTheTowerCollider(this))
            {
                enemyManager.AddEnemyInsideColliderlist(this);
            }
        }
    }
}
