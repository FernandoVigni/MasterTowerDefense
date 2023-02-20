using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public FireBallManager fireBallManager;
    public List<GameObject> enemyList = new List<GameObject>();
    public float countDown;
    Vector3 objetive;

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(enemyList != null && enemyList.Count > 0  && countDown <= 0)
        {
            countDown = 2;
            //Vector3 initPosition = transform.position;
            objetive = GetNearestEnemyPosition();
            Debug.Log(fireBallManager.fireBalls.Count);
            fireBallManager.ShootNewFireball(objetive);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Agregamos el objeto "Enemy" a la lista si no está ya en ella
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);
            }

            // lo que sigue a bajo hay q cambiarlo , cuando impacta hace daño y se va la bola a su pool y se desactiva el movimiento.
        
            // Si la vida del objeto "Enemy" llega a 0, lo eliminamos de la lista
            if (other.gameObject.GetComponent<Enemy>().life <= 0)
            {
                enemyList.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Eliminamos el objeto "Enemy" de la lista si está en ella
            if (enemyList.Contains(other.gameObject))
            {
                enemyList.Remove(other.gameObject);
            }
        }
    }

    public Vector3 GetNearestEnemyPosition()
    {
        Debug.Log("Esta entrando a GetNearestEnemy.");
        // Ordenamos la lista de enemigos por distancia
        enemyList.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        // Devolvemos el primer enemigo de la lista, si la lista no está vacía
        if (enemyList.Count > 0)
        {
            Debug.Log("Esta entrando aca?");
            return enemyList[0].transform.position;
        }
        return Vector3.zero;
    }
}
