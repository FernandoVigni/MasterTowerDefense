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
    public Animator animator;
    public LifeBar lifeBar;
    public EnemyManager enemyManager;
    System.Random random = new System.Random();
    public float resetTimeToAtack;
    public float timeToAtack;

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
    public bool isAtacking = false;
    public float distanceToTower;
    public float coefficient;
    public Vector3 scale;
    public GameObject meleHit;
    public GameObject rangeHit;

    public void AtackTower(float physicalDamage,float magicDamage) 
    {
        tower.GetDamage(physicalDamage, magicDamage);
    }

    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        //  animator = GetComponent<Animator>();
        randomNumberToSort = random.Next(1, 10000);
    }

    void Update()
    {
        timeToAtack -= Time.deltaTime;
        if (isAtacking && timeToAtack <= 0) 
        {
            AtackTower(physicalDamage, magicDamage);

            meleHit.SetActive(true);


            timeToAtack = resetTimeToAtack;
            if (!animator.GetBool("Atack"))
            {
                animator.SetBool("Run", false);
                animator.SetBool("Atack 0", true);
            }
        }

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

    public void ActivateAtackAnimation()
    {
        // validar si este objeto tiene el Script "Warrior" o "Giant" si lo tiene hacer XXXXXX

        // Validar si este objeto tiene el Script "Mage" si lo tiene hacer YYYYYYY

        if (GetComponent<Warrior>() != null || GetComponent<Giant>() != null)
        {
            meleHit.SetActive(false);
            meleHit.SetActive(true);
        }
        
      /*  if (GetComponent<Mage>() != null)
        {
            rangeHit.SetActive(false);
            rangeHit.SetActive(true);
        }
      */
    }

    public void SetMaxLife(float maxLife)
    {
        this.maxLife = maxLife;
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
        if (distanceToTower > 75 && (GetComponent<Giant>() != null || GetComponent<Warrior>() != null))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isAtacking = false;
            isWalking = true;
        }

        if (distanceToTower < 75 && (GetComponent<Giant>() != null || GetComponent<Warrior>() != null))
        {
            isAtacking = true;
            isWalking = false;
            //aqui el animator tiene que hacer el ataque
        }

        if (distanceToTower > 110 && GetComponent<Mage>() != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isAtacking = false;
            isWalking = true;
        }

        if (distanceToTower < 110 && GetComponent<Mage>() != null)
        {
            //aqui el animator tiene que hacer el ataque
            isAtacking = true;
            isWalking = false;
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

    public void ReceibeDamage(float damage)
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
