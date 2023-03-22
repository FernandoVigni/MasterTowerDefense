using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public FireBallManager fireBallManager;
    public float speed;
    public int damage;
    public bool translate = false;

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

    public void StopMove()
    {
        translate = false;
    }

    public void LookAt(Enemy enemy)
    {
        transform.LookAt(enemy.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            

            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy");
                other.GetComponent<Enemy>().ReceibeDamage(damage);
            }

            // Nunca reconoce el Ground
            if (other.gameObject.CompareTag("Ground"))
                { Debug.Log("Ground"); }
            
            StopMove();
            fireBallManager.MoveToInitialZone(this);

        }
    }
}
