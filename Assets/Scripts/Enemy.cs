using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public int life;
    public bool isWalking = true;
    public float speed = 5f; // velocidad a la que se mueve el enemigo hacia la torre
    private Transform target; // objeto de destino, en este caso la torre
    public float distanceBetweenEnemyAndTower; // variable para almacenar la distancia al objetivo

    public void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Tower").transform;
    }

    private void Update()
    {
        if(isWalking)
            Walk();
        
        // Calcula la distancia entre el enemigo y la torre
        distanceBetweenEnemyAndTower = enemyManager.GetDistanceBetweenEnemyAndTower(transform.position);
        if(life <= 0)
            Defet();
    }

 private void Walk()
 {
    if (target != null && distanceBetweenEnemyAndTower > 2 )
    {
        Debug.Log("Entro!!");
        transform.LookAt(target); // mira hacia el objetivo
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // se mueve hacia el objetivo
    }
 }

    public void DealDamage(int damage)
    {
        life -= damage;
    }

    private void Defet()
    {
        isWalking = false;
        transform.position = new Vector3(-40f, 0f, -40f);
    }
}
