using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform positionToInstantiateWarrior;
    public Transform positionToInstantiateMage;
    public Transform positionToInstantiateGiant;
    public Warrior warrior;
    public Mage mage;
    public Giant giant;
    public Tower tower;
    public List<Enemy> listOfEnemiesToDefeatInThisStage = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();

    public Enemy GetNearestEnemy()
    {
        if (listOfEnemiesInsideTheTowerCollider.Count > 1)
        {
            listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
            return listOfEnemiesInsideTheTowerCollider[0];
        }
        if (listOfEnemiesInsideTheTowerCollider.Count == 1)
            return listOfEnemiesInsideTheTowerCollider[0];

        return null;
    }

    public void InstantiateWarrior()
    {
        Warrior newWarriorEnemy = Instantiate(warrior, positionToInstantiateWarrior.position, Quaternion.identity);
        EnemySet(newWarriorEnemy);
    }

    public void InstantiateMage()
    {
        Mage newMageEnemy = Instantiate(mage, positionToInstantiateMage.position, Quaternion.identity);
        EnemySet(newMageEnemy);
    }

    public void InstantiateGiant()
    {
        Mage newGiantEnemy = Instantiate(mage, positionToInstantiateGiant.position, Quaternion.identity);
        EnemySet(newGiantEnemy);
    }

    public void EnemySet(Enemy enemy)
    {
        enemy.isWalking = false;
        enemy.OnDeath += OnEnemyDeath;
        listOfEnemiesToDefeatInThisStage.Add(enemy);
    }
    
    public void OnEnemyDeath(Enemy enemy) 
    {
        listOfEnemiesToDefeatInThisStage.Remove(enemy);
        Destroy(enemy);
        //Chequeamos que no queden mas enemigos aqui?
    }
}
