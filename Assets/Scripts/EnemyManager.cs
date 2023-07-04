using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    private void Awake()
    {
        tower = FindObjectOfType<Tower>();
    }

    public Transform positionToInstantiateEnemies;
    public Tower tower;
    public GoldStatus goldStatus;
    public GameObject pointALeftPortal;
    public GameObject pointBLeftPortal;
    public GameObject pointARightPortal;
    public GameObject pointBRightPortal;
    public float coefficient;
    public float distanceToInstanciateEnemyToTower;
    public int delayToInstantiateEnemy;

    public Warrior warrior;
    public Mage mage;
    public Runner runner;
    public Giant giant;
    public Boss boss;

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
        coefficient = PhaseManager.instance.GetCoefficient();
        enemy.SetCoefficient(coefficient);  
        enemy.isWalking = false;
        enemy.OnDeath += OnEnemyDeath;
        listOfEnemiesToDefeatInThisPhase.Add(enemy);
    }

    // List Methods
    public void ShufleList(List<Enemy> list)
    {
        listOfEnemiesToDefeatInThisPhase.Sort((x, y) => UnityEngine.Random.Range(0, 2));
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
        listOfEnemiesInsideTheTowerCollider.Sort(new DistanceComparer(transform.position));
    }

    private class DistanceComparer : IComparer<Enemy>
    {
        private readonly Vector3 centerPoint;

        public DistanceComparer(Vector3 centerPoint)
        {
            this.centerPoint = centerPoint;
        }

        public int Compare(Enemy a, Enemy b)
        {
            if (a != null || b != null && a.isActiveAndEnabled)
            {
                float distanceA = Vector3.Distance(a.transform.position, centerPoint);
                float distanceB = Vector3.Distance(b.transform.position, centerPoint);
  
                return (int)distanceA.CompareTo(distanceB);
            }
            return 150;
        }
    }
        
    //---------------------------------------

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

        goldStatus.RecibeGold(enemy.goldValueOnDeath);
        recivedGoldInThisPhase += enemy.goldValueOnDeath;

        if (listOfEnemiesToDefeatInThisPhase.Count <= 0 && enemiesSentList.Count <= 0)
        {
            float bagOfGold = PhaseManager.instance.GetAmountBagOfGold();
            PhaseManager.instance.GetAmountBagOfGold();

            Debug.Log("You obtain in this phase: " + recivedGoldInThisPhase + " Gold");
            recivedGoldInThisPhase = 0;

            if (PhaseManager.instance.nextPhase())
            {
                enemy.DestroyEnemy();
                Debug.Log("esta terminando la Phase: " + PhaseManager.instance.currentPhase);
                //Ver si aqui se podrian buscart todos los enemyesws y borrarlos HF
                // PhaseManager.Instance.PlayMusic();
                PhaseManager.instance.SetPhasePlusOne();
                Debug.Log("Inicia la phase: " + PhaseManager.instance.currentPhase + 1);



                PhaseManager.instance.StartPhase();
            
            
            
            }
            else
            {
                Debug.Log("End of all Phases");
                /*End Phases*/ 
            }
        }
        enemy.DestroyEnemy();
    }

    public async void SendEnemiesLeftPortal()
    {
        int enemiesInThisLevel = GetAmmountOflistOfEnemiesToDefeatInThisPhase();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemiesInThisLevel > 0)
            {
                ShufleList(listOfEnemiesToDefeatInThisPhase);
                Enemy enemy = GetFirstEnemyFromPhaseList();

                if (enemy != null)
                {
                    Vector3 leftSpawnPositionRandom = new Vector3();
                    leftSpawnPositionRandom = GenerateRandomPointLeftPortal();
                    enemy.transform.position = leftSpawnPositionRandom;
                    enemy.LookTower();
                    enemy.StartMove();
                    RemoveEnemyFromPhase(enemy);
                    AddEnemyToSentList(enemy);
                    await Task.Delay(1500);
                }
            }
        }
    }

    public async void SendEnemiesRightPortal()
    {
        int enemiesInThisLevel = GetAmmountOflistOfEnemiesToDefeatInThisPhase();
        for (int i = 0; i < enemiesInThisLevel; i++)
        {
            if (enemiesInThisLevel > 0)
            {
                ShufleList(listOfEnemiesToDefeatInThisPhase);
                Enemy enemy = GetFirstEnemyFromPhaseList();

                if (enemy != null)
                {
                    Vector3 rightSpawnPositionRandom = new Vector3();
                    rightSpawnPositionRandom = GenerateRandomPointRightPortal();
                    enemy.transform.position = rightSpawnPositionRandom;
                    enemy.LookTower();
                    enemy.StartMove();
                    RemoveEnemyFromPhase(enemy);
                    AddEnemyToSentList(enemy);
                    await Task.Delay(1500);
                }
            }
        }
    }

    public Vector3 GenerateRandomPointLeftPortal()
    {
        float randomX = UnityEngine.Random.Range(pointALeftPortal.transform.position.x, pointBLeftPortal.transform.position.x);
        float randomY = UnityEngine.Random.Range(pointALeftPortal.transform.position.y, pointBLeftPortal.transform.position.y);
        float randomZ = UnityEngine.Random.Range(pointALeftPortal.transform.position.z, pointBLeftPortal.transform.position.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public Vector3 GenerateRandomPointRightPortal()
    {
        float randomX = UnityEngine.Random.Range(pointARightPortal.transform.position.x, pointBRightPortal.transform.position.x);
        float randomY = UnityEngine.Random.Range(pointARightPortal.transform.position.y, pointBRightPortal.transform.position.y);
        float randomZ = UnityEngine.Random.Range(pointARightPortal.transform.position.z, pointBRightPortal.transform.position.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
