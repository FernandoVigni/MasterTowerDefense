using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public FireBallManager fireBallManager;
    public float speed;
    public int damage;
    public bool translate = false;
    public GameObject explosionOne;
    public GameObject explosionTwo;

    void Update()
    {
        if (translate)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetSpeed(int speed) 
    {
        this.speed = speed;
    }

    public void SetDamage(int damage) 
    {
        this.damage = damage;
    }
    
    public void StartMove()
    {
        translate = true;
    }
    private void StopMove()
    {
        translate = false;
    }

    public void Move()
    {
        translate = false;
    }

    public void LookAt(Enemy enemy)
    {
        if (enemy == null)
            return;

        transform.LookAt(enemy.transform.position);
    }

    public void InstantiateRandomExplosion()
    {
        int randomNumber = GenerateRandomNumber();

        if (randomNumber == 1)
        {
            ExploteFireBallOne();
        }
        else if (randomNumber == 2)
        {
            ExploteFireBallTwo();
        }
    }

    private int GenerateRandomNumber()
    {
        // Genera un número aleatorio entre 1 y 2
        return Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
    }

    public async Task ExploteFireBallOne() 
    {
        GameObject explosion = Instantiate(explosionOne, transform.position, Quaternion.identity);
        await Task.Delay(2000);
        Destroy(explosion);
    }

    public async Task ExploteFireBallTwo()
    {
        GameObject explosion = Instantiate(explosionTwo, transform.position, Quaternion.identity);
        await Task.Delay(2000);
        Destroy(explosion);
    }

    private void OnTriggerEnter(Collider other)
    {
        StopMove();
        InstantiateRandomExplosion();
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().ReceibeDamage(damage);
            }
        }
        fireBallManager.MoveToInitialZone(this);
    }
}
