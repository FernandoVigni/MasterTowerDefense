using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();
    public List<Enemy> listOfEnemiesOutsideTheTowerCollider = new List<Enemy>();
    public Transform pointOfSpawnEnemies;
    
/*
        public void InstantiateEnemyOutsideCollider()
        {
            for (int i = 0; i < numFireBalls; i++)
            {
                Debug.Log("creada al " + i + "° Enemigo");
                // Creamos una variable para guardar la posición en la que queremos instanciar el objeto
                positionToInstantiate = new Vector3(0, 8f, 0f); // Cambia los valores por las coordenadas que necesites

                FireBall fireBall = Instantiate(fireBallPrefab, positionToInstantiate,  Quaternion.identity);
                //fireBall.SetActive(false);
                fireBall.translate = false;
                fireBalls.Add(fireBall);

                ammountOFireBalls = fireBalls.Count;
                Debug.Log("hay en total " + ammountOFireBalls + " Fire Balls pipipi" );
            }

        //Instanciar un enemigo y meterlo a la lista de outside

        // instanciar un enemigo en modo reposo
        // agregarlo a la lista de Outisde

*/
    public Vector3 GetNearestEnemyPosition()
    {
        Debug.Log("Esta entrando a GetNearestEnemy.");
        // Ordenamos la lista de enemigos por distancia
        listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        // Devolvemos el primer enemigo de la lista, si la lista no está vacía
        if (listOfEnemiesInsideTheTowerCollider.Count > 0)
        {
            return listOfEnemiesInsideTheTowerCollider[0].transform.position;
        }
        Debug.Log("La Lista de enemigos /enemyList/ estaba vacia");
        return Vector3.zero;
    }
    
    public void CheckEnemyContainedInsideList(Enemy enemy)
    {
         if (!listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            if (listOfEnemiesOutsideTheTowerCollider.Contains(enemy))
                RemoveEnemyFromTheOutsideList(enemy);

            listOfEnemiesInsideTheTowerCollider.Add(enemy);
        }   
    }

    public void RemoveEnemyFromTheInsideList(Enemy enemy)
    {
            // Eliminamos el objeto "Enemy" de la lista si está en ella
        if (listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            listOfEnemiesInsideTheTowerCollider.Remove(enemy);
            listOfEnemiesOutsideTheTowerCollider.Add(enemy);           
        }
    }

    public void RemoveEnemyFromTheOutsideList(Enemy enemy)
    {
            // Eliminamos el objeto "Enemy" de la lista si está en ella
        if (listOfEnemiesOutsideTheTowerCollider.Contains(enemy))
        {
            listOfEnemiesOutsideTheTowerCollider.Remove(enemy);
            listOfEnemiesInsideTheTowerCollider.Add(enemy);
        }
    }

    public void RemoveAllEnemyesFromAllLists()
    {
        listOfEnemiesInsideTheTowerCollider.Clear();
        listOfEnemiesOutsideTheTowerCollider.Clear();
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

    public void InititateEnemy(Enemy enemy)
    {
        enemy.SetTarget();
        enemy.isWalking = true;
    }

    public void ActivateFirstEnemyInListInitialized()
    {
        Enemy enemy = listOfEnemiesInsideTheTowerCollider[0];
        InititateEnemy(enemy);
    }
}
