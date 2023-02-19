using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;

    public float speed = 5f; // velocidad a la que se mueve el enemigo hacia la torre
    private Transform target; // objeto de destino, en este caso la torre
    public float distanceToTarget; // variable para almacenar la distancia al objetivo

    private void Start()
    {
        // Busca el objeto "Tower" en el mapa y establece su transform como el objetivo
        target = GameObject.FindGameObjectWithTag("Tower").transform;
    }

    private void Update()
    {
        // Si el objetivo existe, mueve el enemigo hacia él
        if (target != null && distanceToTarget > 2)
        {
            transform.LookAt(target); // mira hacia el objetivo
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // se mueve hacia el objetivo
        }
        
        // Calcula la distancia entre el enemigo y la torre
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Imprime la distancia actual en la consola
        Debug.Log("Distancia a la torre: " + distanceToTarget);
    }
}
