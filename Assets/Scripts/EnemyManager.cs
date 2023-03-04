using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{/*
    public Transform positionToInstantiateWarrior;
    public Transform positionToInstantiateMage;
    public Transform positionToInstantiateGiant;
    */
    public Transform positionToInstantiateEnemies;
    public Transform PointToDiscardEnemies;

    public Warrior warrior;
    public Mage mage;
    public Giant giant;
    public Kamikaze kamikaze;

    public List<Enemy> listOfEnemiesDefeated = new List<Enemy>();
    public List<Enemy> listOfEnemiesToDefeatInThisStage = new List<Enemy>();
    public List<Enemy> enemiesSentList = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();

    // Instantiate Enemies
    /*
    public void InstantiateEnemy(Type enemyType) 
    {
        Enemy newEnemy = (Enemy)Activator.CreateInstance(enemyType);
        EnemySet(newEnemy);
    }
    */

    /*
// Crear un objeto de juego (GameObject) en la escena de Unity
GameObject newWarriorObject = new GameObject("NewWarrior");

// Asociar el componente Enemy a este objeto de juego
newWarriorObject.AddComponent<Enemy>();

// Asociar la instancia de Warrior creada anteriormente a este objeto de juego
newWarriorObject.GetComponent<Enemy>().SetEnemyType(newWarrior);

// Colocar el objeto de juego en una posición adecuada en el mundo del juego
newWarriorObject.transform.position = new Vector3(0, 0, 0);
     * -----------------------
     * 
     * */
    public void InstantiateWarrior()
    {
        Warrior newWarriorEnemy = Instantiate(warrior, positionToInstantiateEnemies.position, Quaternion.identity);
        EnemySet(newWarriorEnemy);
    }

    public void InstantiateMage()
    {
        Mage newMageEnemy = Instantiate(mage, positionToInstantiateEnemies.position, Quaternion.identity);
        EnemySet(newMageEnemy);
    }

    public void InstantiateGiant()
    {
        Giant newGiantEnemy = Instantiate(giant, positionToInstantiateEnemies.position, Quaternion.identity);
        EnemySet(newGiantEnemy);
    }

    public void InstantiateKamikaze()
    {
        Kamikaze newKamikazeEnemy = Instantiate(kamikaze, positionToInstantiateEnemies.position, Quaternion.identity);
        EnemySet(newKamikazeEnemy);
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

    public void RemoveAllInStage()
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

    //Enemies Defet List
    public void AddEnemyToListOfEnemyDefeated(Enemy enemy)
    {
        if (!listOfEnemiesDefeated.Contains(enemy))
           listOfEnemiesDefeated.Add(enemy);
    }

    public void RemoveAllEnemiesDefeated() 
    {
        listOfEnemiesDefeated.RemoveAll(Enemy => true) ;
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
        AddEnemyToListOfEnemyDefeated(enemy);
        RemoveEnemiesInsideColliderList(enemy);
        RemoveEnemyFromSentList(enemy);
        enemy.StopMove();
    }
}
