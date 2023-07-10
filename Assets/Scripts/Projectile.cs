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
    //public GameObject explosionTwo;

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

    public void 
        Move()
    {
        translate = false;
    }

    public void LookAt(Enemy enemy)
    {
        if (enemy == null)
            return;

            transform.LookAt(enemy.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        StopMove();
        InstantiateRandomExplosion();
        fireBallManager.MoveToInitialZone(this);
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().ReceibeDamage(damage);
            }
        }
    }

    public void InstantiateRandomExplosion()
    {
        int randomNumber = GenerateRandomNumber();

        if (randomNumber == 1)
        {
            EploteFireBallOne();
        }
        else if (randomNumber == 2)
        {
            EploteFireBallTwo();
        }
    }

    private int GenerateRandomNumber()
    {
        // Genera un número aleatorio entre 1 y 2
        return Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
    }

    public async Task EploteFireBallOne() 
    {
        GameObject explosion = Instantiate(explosionOne, transform.position, Quaternion.identity);
        await Task.Delay(2000);
        Destroy(explosion);
    }

    public async Task EploteFireBallTwo()
    {
        GameObject explosion = Instantiate(explosionTwo, transform.position, Quaternion.identity);
        await Task.Delay(2000);
        Destroy(explosion);
    }
}
