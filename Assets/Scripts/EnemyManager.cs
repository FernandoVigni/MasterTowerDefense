using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    private void Awake()
    {
        phaseManager = FindObjectOfType<PhaseManager>(); 
        tower = FindObjectOfType<Tower>();
    }

    public Transform positionToInstantiateEnemies;
    public PhaseManager phaseManager;
    public Tower tower;
    public float coefficient;
    public float distanceToInstanciateEnemyToTower;
    public int delayToInstantiateEnemy;
    public int phase = 0;

    public Warrior warrior;
    public Mage mage;
    public Runner runner;
    public Giant giant;

    public List<Enemy> enemiesSentList = new List<Enemy>();
    public List<Enemy> listOfEnemiesToDefeatInThisPhase = new List<Enemy>();
    public List<Enemy> listOfEnemiesInsideTheTowerCollider = new List<Enemy>();

    public void InstantiateWarrior()
    {
        Warrior newWarriorEnemy = Instantiate(warrior, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newWarriorEnemy);
    }

    public void InstantiateMage()
    {
        Mage newMageEnemy = Instantiate(mage, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newMageEnemy);
    }

    public void InstantiateRunner()
    {
        Runner newRunnerEnemy = Instantiate(runner, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newRunnerEnemy);
    }

    public void InstantiateGiant()
    {
        Giant newGiantEnemy = Instantiate(giant, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newGiantEnemy);
    }

    // Stage List Methods
    public int GetAmmountOflistOfEnemiesToDefeatInThisPhase() 
    {
        int result;
        result = listOfEnemiesToDefeatInThisPhase.Count;
        return result;
    }

    public Enemy GetFirstEnemyFromPhaseList()
    {
        if (listOfEnemiesToDefeatInThisPhase.Count >= 1)
        {
            Enemy iEnemy = listOfEnemiesToDefeatInThisPhase[0];
            return iEnemy;
        }
        return null;
    }

    public void SetEnemy(Enemy enemy)
    {
        coefficient = phaseManager.GetCoefficient();
        enemy.SetCoefficient(coefficient);  
        enemy.isWalking = false;
        enemy.OnDeath += OnEnemyDeath;
        listOfEnemiesToDefeatInThisPhase.Add(enemy);
    }

    // List Methods
    public void ShufleList(List<Enemy> list)
    {
        listOfEnemiesToDefeatInThisPhase.Sort((x, y) => UnityEngine.Random.Range(-1, 1));
    }

    public void RemoveEnemyFromPhase(Enemy enemy) 
    {
        listOfEnemiesToDefeatInThisPhase.Remove(enemy);
    }

    public void RemoveAllInStage()
    {
        listOfEnemiesToDefeatInThisPhase.RemoveAll(Enemy => true);
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
        // Preguntarle al chat como hago para validar que alguno quedo en nullo y si lo hay como se quita.
        listOfEnemiesInsideTheTowerCollider.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position)
            .CompareTo(Vector3.Distance(b.transform.position, transform.position)));
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
    public int recivedGoldInThisPhase;

    public void OnEnemyDeath(Enemy enemy)
    {
        RemoveEnemyFromSentList(enemy);
        RemoveEnemiesInsideColliderList(enemy);

        tower.RecibeGold(enemy.goldValueOnDeath);
        recivedGoldInThisPhase += enemy.goldValueOnDeath;

        if (listOfEnemiesToDefeatInThisPhase.Count <= 0 && enemiesSentList.Count <= 0)
        {
            Debug.Log("Do you obtain in this phase: " + recivedGoldInThisPhase + " Gold");
            recivedGoldInThisPhase = 0;

            float bagOfGold = phaseManager.GetAmountBagOfGold();
            
            tower.RecibeGold(bagOfGold);

            if (phaseManager.nextPhase())
                { phaseManager.SetPhasePlusOne(); }
            else
                { /*End Phases*/ }
        }
        enemy.DestroyEnemy();
    }

    public async void SendEnemies()
    {
        int enemiesInThisLevel = GetAmmountOflistOfEnemiesToDefeatInThisPhase();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemiesInThisLevel > 0)
            {
                ShufleList(listOfEnemiesToDefeatInThisPhase);
                Enemy enemy = GetFirstEnemyFromPhaseList();
                await Task.Delay(delayToInstantiateEnemy);

                // 1) random 0 - 360      90
                int randonAngle = GetRandomNumber(90, 180);

                // 2) calculo sin         
                double x = GetSineOfAnAngle(randonAngle);
                float floatX = (float)x * distanceToInstanciateEnemyToTower;

                // 3) calculo cos         
                double z = GetCosOfAnAngle(randonAngle);
                float floatZ = (float)z * distanceToInstanciateEnemyToTower;

                Vector3 spawnPositionRandom = new Vector3(floatX, 4, floatZ);
                enemy.transform.position = spawnPositionRandom;
                enemy.LookTower();
                enemy.StartMove();
                RemoveEnemyFromPhase(enemy);
                AddEnemyToSentList(enemy);
            }
        }
    }

    // Trigonometry to choose a random point on a circle circumference.
    public double GetSineOfAnAngle(int angle)
    {
        double angleInRadians = angle * Math.PI / 180;
        double sineValue = Math.Sin(angleInRadians);
        sineValue *= distanceToInstanciateEnemyToTower;
        sineValue = Math.Round(sineValue, 2);
        return sineValue;
    }

    public double GetCosOfAnAngle(int angle)
    {
        double angleInRadians = angle * Math.PI / 180;
        double cosValue = Math.Cos(angleInRadians);
        cosValue = cosValue * distanceToInstanciateEnemyToTower;
        cosValue = Math.Round(cosValue, 2);
        return cosValue;
    }

    public int GetRandomNumber(int min, int max)
    {
        int randomNumber = UnityEngine.Random.Range(min, max);
        return randomNumber;
    }
}
