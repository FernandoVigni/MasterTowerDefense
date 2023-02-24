using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public EnemyManager enemyManager;
    public bool translate = false;
    public int damage;
    public float speed = 5f;

    public void AbleToTranslate()
    {
        translate = true;       
    }

    public void LookAt(Vector3 objetive)
    {
        transform.LookAt(objetive);
    }


    void Update()
    {
        if(translate)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    } 

    public Vector3 resetPosition = new Vector3(-40f, 0f, -40f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReceibeDamage(damage);
            }
        }
        transform.position = resetPosition;
    }
}
