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

    public void MoveToInitialZone()
    {
        this.transform.position = fireBallManager.positionToInstantiate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground"))
        {
            StopMove();
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().ReceibeDamage(damage);
                MoveToInitialZone();
            }
            else
            {
                MoveToInitialZone();
            }
        }
    }
}
