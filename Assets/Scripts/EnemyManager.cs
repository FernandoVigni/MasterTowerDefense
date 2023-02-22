using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> listOfEnemiesToDefeatInThisWave = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();
    public Transform positionToInstantiateWarrior;
    public Enemy enemy;
        
    public Vector3 GetNearestEnemyPosition()
    {
        // Ordenamos la lista de enemigos por distancia
        listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        // Devolvemos el primer enemigo de la lista, si la lista no está vacía
        if (listOfEnemiesInsideTheTowerCollider.Count > 0)
        {
            return listOfEnemiesInsideTheTowerCollider[0].transform.position;
        }
        return Vector3.zero;
    }
    
    public void CheckEnemyContainedInsideList(Enemy enemy)
    {
        if (!listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            listOfEnemiesInsideTheTowerCollider.Add(enemy);
        }   
    }

    public void RemoveEnemyFromTheInsideList(Enemy enemy)
    {
        if (listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            listOfEnemiesInsideTheTowerCollider.Remove(enemy);          
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

    public void StartLevel(int ammountOfWarriorsInWave,int ammountOfMagesInWave, int ammountOfGigantsInWave)
    {
        for (int i = 0; i < ammountOfWarriorsInWave; i++)
        {
            InstantiateWarrior();
        }

      /*  for (int i = 0; i < ammountOfMagesInWave; i++)
        {
            InstantiateMage();
        }

        for (int i = 0; i < ammountOfGigantsInWave; i++)
        {
            InstantitateGiant();
        }*/
    }
    
    private void InstantiateWarrior()
    {
        Debug.Log("Se esta instanciando");
        Enemy newWarriorEnemy = Instantiate(enemy, positionToInstantiateWarrior.position, Quaternion.identity);
        SetWarriorStatsInEnemy(newWarriorEnemy);
        // deberia agregarse aqui los graficos del warrior.
        listOfEnemiesToDefeatInThisWave.Add(newWarriorEnemy);
    }

    private void SetWarriorStatsInEnemy(Enemy enemy)
    {
        enemy.life = 100;
        enemy.speed = 3;
        enemy.armor = 10;
        // si tiene alguna habilidad especial va aqui
    }
}
