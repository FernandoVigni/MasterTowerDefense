using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform positionToInstantiateWarrior;
    public Transform positionToInstantiateMage;
    public Transform pointToInstantiateGiant;
    public Warrior warrior;
    public Mage mage;
    public Tower tower;

    public int ammountOfEnemiesInsideTowerCollider;
    //Todo , borrar el harcodeo.
    public Vector3 pointToTranslateEnemy;
    public List<Enemy> listOfEnemiesToDefeatInThisStage = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();

    public Vector3 GetNearestEnemyPosition()
    {
        if(listOfEnemiesInsideTheTowerCollider.Count > 1)
            listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));

        if (listOfEnemiesInsideTheTowerCollider.Count > 0)
            return listOfEnemiesInsideTheTowerCollider[0].transform.position;
            
        return Vector3.zero;
    }
    
    public void CheckEnemyContainedInsideList(Enemy enemy)
    {
        if (!listOfEnemiesInsideTheTowerCollider.Contains(enemy))
        {
            listOfEnemiesToDefeatInThisStage.Remove(enemy);
            listOfEnemiesInsideTheTowerCollider.Add(enemy);
        }   
    }

    public void RemoveEnemyFromInsideList(Enemy enemyToRemove)
    {
        listOfEnemiesInsideTheTowerCollider.Remove(enemyToRemove);
    }

    public void InstantiateWarrior()
    {
        Warrior newWarriorEnemy = Instantiate(warrior, positionToInstantiateWarrior.position, Quaternion.identity);
        newWarriorEnemy.isWalking = false;
        listOfEnemiesToDefeatInThisStage.Add(newWarriorEnemy);
        MoveEnemyToInitialPosition(newWarriorEnemy);
    }

    public void InstantiateMage()
    {
        Mage newMageEnemy = Instantiate(mage, positionToInstantiateMage.position, Quaternion.identity);
        newMageEnemy.isWalking = false;
        listOfEnemiesToDefeatInThisStage.Add(newMageEnemy);
        MoveEnemyToInitialPosition(newMageEnemy);
    }



    public void MoveEnemyToInitialPosition(Enemy enemy)
    {
       //esto esta mal , desarrollar
        int angle = GetRandomAngle();
        Vector3 centerPoint = new Vector3(0, 0, 0);
        transform.position = pointToTranslateEnemy;
        enemy.isWalking = true;
    }


    public int GetRandomAngle()
    {
        return UnityEngine.Random.Range(1, 361);
    }
}
