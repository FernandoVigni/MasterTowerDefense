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
        enemy.OnDeath += enemy.OnEnemyDeath;
        listOfEnemiesToDefeatInThisStage.Add(enemy);
    }
}
