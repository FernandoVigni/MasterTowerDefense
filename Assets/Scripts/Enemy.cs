using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public int life;
    public float speed; // velocidad a la que se mueve el enemigo hacia la torre
    public int armor;
    public bool isWalking = true;

    public Transform target; // objeto de destino, en este caso la torre
    public float distanceBetweenEnemyAndTower; // variable para almacenar la distancia al objetivo

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
            transform.LookAt(target); // mira hacia el objetivo
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // se mueve hacia el objetivo
        }
    }

    private void Defet()
    {
        enemyManager.RemoveEnemyFromInsideList(this);
        isWalking = false;
        transform.position = new Vector3(-40f, 0f, -40f);

    }
}
