using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Enemy> enemyList = new List<Enemy>();

    public Vector3 GetNearestEnemyPosition()
    {
        Debug.Log("Esta entrando a GetNearestEnemy.");
        // Ordenamos la lista de enemigos por distancia
        enemyList.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        // Devolvemos el primer enemigo de la lista, si la lista no está vacía
        if (enemyList.Count > 0)
        {
            return enemyList[0].transform.position;
        }
        Debug.Log("La Lista de enemigos /enemyList/ estaba vacia");
        return Vector3.zero;
    }
    
    public void CheckEnemyContainedInList(Enemy enemy)
    {
         if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }   
    }

    public void RemoveEnemyFromTheList(Enemy enemy)
    {
            // Eliminamos el objeto "Enemy" de la lista si está en ella
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    public float GetDistanceBetweenEnemyAndTower(Vector3 enemyPosition)
    {
        float distanceBetweenEnemyAndTower;
        Transform target = GameObject.FindGameObjectWithTag("Tower").transform;
        return distanceBetweenEnemyAndTower = Vector3.Distance(enemyPosition, target.position);
    }

    public void DealDamage(Enemy enemy, int damage)
    {
        Debug.Log("Hizo Daño!! Urra!!");
        enemy.life -= damage;
    }

    public void InititateEnemy()
    {
        Enemy enemy = GetFirstEnemyInList();
    }

    public Enemy GetFirstEnemyInList()
    {
        return enemyList[0];
    }
}
