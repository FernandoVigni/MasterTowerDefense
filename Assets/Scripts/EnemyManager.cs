using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    public Transform positionToInstantiateWarrior;
    public Transform positionToInstantiateMage;
    public Transform positionToInstantiateGiant;
    public Transform PointToDiscardEnemies;
    public Warrior warrior;
    public Mage mage;
    public Giant giant;

    public List<Enemy> enemiesSentList = new List<Enemy>();
    public List<Enemy> listOfEnemiesToDefeatInThisStage = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();

    // Instantiate Enemies
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
        Giant newGiantEnemy = Instantiate(giant, positionToInstantiateGiant.position, Quaternion.identity);
        EnemySet(newGiantEnemy);
    }

    // Stage List Methods
    public int GetAmmountOflistOfEnemiesToDefeatInThisStage() 
    {
        int result;
        result = listOfEnemiesToDefeatInThisStage.Count;
        return result;
    }

    public Enemy GetFirstEnemyFromStageList()
    {
        if (listOfEnemiesToDefeatInThisStage.Count >= 1)
        {
            Enemy iEnemy = listOfEnemiesToDefeatInThisStage[0];
            return iEnemy;
        }
        return null;
    }

    public void EnemySet(Enemy enemy)
    {
        enemy.isWalking = false;
        enemy.OnDeath += OnEnemyDeath;
        listOfEnemiesToDefeatInThisStage.Add(enemy);
    }

    public void ShufleList(List<Enemy> list)
    {
        listOfEnemiesToDefeatInThisStage.Sort((x, y) => UnityEngine.Random.Range(-1, 1));
    }

    public void RemoveEnemyFromStageList(Enemy enemy) 
    {
        listOfEnemiesToDefeatInThisStage.Remove(enemy);
    }

    public void  RemoveAllInStage()
    {
        listOfEnemiesToDefeatInThisStage.RemoveAll(Enemy => true);
    }

    // Enemies Sent List Method
    public void AddEnemyToSentList(Enemy enemy)
    {
        enemiesSentList.Add(enemy);
    }

    public void RemoveEnemyFromSentList(Enemy enemy) 
    {
        enemiesSentList.Remove(enemy);
    }

    // Collider List Methods
    public bool IsCointaind(Enemy enemy) 
    {
        return listOfEnemiesInsideTheTowerCollider.Contains(enemy);
    }

    public int GetAmmountOflistOfEnemiesInsideTheTowerCollider()
    {
        int ammountOfEnemies = listOfEnemiesInsideTheTowerCollider.Count;
        return ammountOfEnemies;
    }
    public void SortlistOfEnemiesInsideTheTowerCollider()
    {
        listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
    }

    public Enemy GetIEnemyFromColliderList(int i)
    {
        if (listOfEnemiesInsideTheTowerCollider.Count >= 1)
        {
            Enemy iEnemy = listOfEnemiesInsideTheTowerCollider[i];
            return iEnemy;
        }
        return null;
    }

    public void AddEnemyInsideColliderlist(Enemy enemy) 
    {
        listOfEnemiesInsideTheTowerCollider.Add(enemy);
    }

    public void RemoveEnemiesInsideColliderList(Enemy enemy)
    {
        listOfEnemiesInsideTheTowerCollider.Remove(enemy);
    }

    public void RemoveAllInCollider()
    {
        listOfEnemiesInsideTheTowerCollider.RemoveAll(Enemy => true);
    }

    //Others
    public void MoveEnemiesToDiscardPoint(Enemy enemy)
    {
        enemy.transform.position = PointToDiscardEnemies.position;
    }

    public void InvokeOnDeath(Enemy enemy) 
    {
        enemy.OnDeath.Invoke(enemy);  
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        MoveEnemiesToDiscardPoint(enemy);
        RemoveEnemiesInsideColliderList(enemy);
        RemoveEnemyFromSentList(enemy);
        enemy.StopMove();
    }
}
