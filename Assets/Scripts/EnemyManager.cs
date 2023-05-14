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
        stageManager = FindObjectOfType<StageManager>(); 
        tower = FindObjectOfType<Tower>();
       // levelSelectorManager = FindObjectOfType<LevelSelectorManager>();
    }

   // public LevelSelectorManager levelSelectorManager;
    public Transform positionToInstantiateEnemies;
    public StageManager stageManager;
    public Tower tower;
    public float coefficient;
    public float distanceToInstanciateEnemyToTower;
    public int delayToInstantiateEnemy;

    public Kamikaze kamikaze;
    public Warrior warrior;
    public Giant giant;
    public Mage mage;

    public List<Enemy> enemiesSentList = new List<Enemy>();
    public List<Enemy> listOfEnemiesToDefeatInThisStage = new List<Enemy>();
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

    public void InstantiateGiant()
    {
        Giant newGiantEnemy = Instantiate(giant, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newGiantEnemy);
    }

    public void InstantiateKamikaze()
    {
        Kamikaze newKamikazeEnemy = Instantiate(kamikaze, positionToInstantiateEnemies.position, Quaternion.identity);
        SetEnemy(newKamikazeEnemy);
    }

    public void SetCurrentCoefficient(float currentCoefficient) 
    {
        this.coefficient = currentCoefficient;
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

    public void SetEnemy(Enemy enemy)
    {
        enemy.SetCoefficient(coefficient);  
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

    public void OnEnemyDeath(Enemy enemy)
    {
        RemoveEnemyFromSentList(enemy);
        RemoveEnemiesInsideColliderList(enemy);
        tower.RecibeGold(enemy.goldValueOnDeath);

        if (listOfEnemiesToDefeatInThisStage.Count <= 0 && enemiesSentList.Count <= 0)
        {
            float bagOfGold = stageManager.GetAmountBagOfGold();
            tower.RecibeGold(bagOfGold);
            //levelSelectorManager.IncreseMaxLevelAviable();
            // ir al selector de Niveles
        }
        enemy.DestroyEnemy();
    }

    public async void SendEnemies()
    {
        int enemiesInThisLevel = GetAmmountOflistOfEnemiesToDefeatInThisStage();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemiesInThisLevel > 0) ;
            {
                ShufleList(listOfEnemiesToDefeatInThisStage);
                Enemy enemy = GetFirstEnemyFromStageList();
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
                RemoveEnemyFromStageList(enemy);
                AddEnemyToSentList(enemy);
            }
        }
    }

    public double GetSineOfAnAngle(int angle)
    {
        double angleInRadians = angle * Math.PI / 180;
        double sineValue = Math.Sin(angleInRadians);
        sineValue = sineValue * distanceToInstanciateEnemyToTower;
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
