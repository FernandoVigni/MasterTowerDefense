using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Tower : MonoBehaviour
{
    public LifeBar1 lifeBar1;
    public GameObject lifeBar1Canva;
    public GameObject towerFire;
    public FireBallManager fireBallManager;
    public EnemyManager enemyManager;
    public Enemy objetive;
    public GameObject towerEffects;
    public ExplosiveMine explosiveMinePrefabA;
    public ExplosiveMine explosiveMinePrefabB;
    public GoldStatus goldStatus;
    public float countDownToShoot;
    public float countDownReset;
    public float manualShoot;
    public float life;
    public float maxLife;
    public float gold;
    public float launchForce;
    public int ammountOfMines;
    public int secondsBetweenMines;
    public bool firstActivation;
    public GameObject flashShootEffect;
    public GameObject minesDeployButton;
    public GameObject effectMinesDeployButton;
    public GameObject efectSparks;
    public GameObject basicCore;
    public GameObject midCore;
    public GameObject bigCore;
    public GameObject finalBigCore;
    public GameObject effectFinalAtackButton;
    public PhaseManager phaseManager;
    public bool firstEndGame;
    public Transform CornerA;
    public Transform CornerB;
    public Transform CornerC;
    public Transform CornerD;

    void Update()
    {
        lifeBar1.SetRemainingLifeToShow(life, maxLife);

        countDownToShoot = countDownToShoot - Time.deltaTime;
        if (countDownToShoot < 0 && enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() > 0)
        {
            Debug.Log("Disparando");
            phaseManager.meteorites.SetActive(false);
            ShootNearestEnemy();
        }

        if (life <= 3200)
        {
            towerFire.SetActive(true);
        }
    }

    public void ResetCore()
    {
        midCore.SetActive(false);
        bigCore.SetActive(false);
        finalBigCore.SetActive(false);
        efectSparks.SetActive(false);
    }

    public void TurnOffLifeBarCanva()
    {
        lifeBar1Canva.SetActive(false);
    }

    public void TurnOnLifeBarCanva()
    {
        lifeBar1Canva.SetActive(true);
    }


    private void Awake()
    {
        effectMinesDeployButton.SetActive(false);
        firstEndGame = true;
    }

    public void ResetFirstActivationOfMinesButton()
    {
        firstActivation = true;
    }

    public void GetDamage(float physicalDamage, float magicDamage)
    {
        life -= (physicalDamage + magicDamage);
        IsTowerDeath();
    }

    private async Task IsTowerDeath()
    {
        if (life <= 0 && firstEndGame)
        {
            firstEndGame = false;
            MainMenu.Instance.Lose();
            //MainMenu.Instance.Win();
        }
    }

    public void ActivatePowerUp()
    {
        manualShoot = 1.8f;
        towerEffects.SetActive(true);
    }


    public void RecibeDamage(float phisicalDamage, float magicDamage)
    {
        float totalDamageToDeal = phisicalDamage + magicDamage;
        this.life -= this.life - totalDamageToDeal;
    }

    public void ShootNearestEnemy()
    {
        if (countDownToShoot < manualShoot)
        {
            Enemy nearestEnemyInsideCollider = GetNearestEnemyInsideCollider();
            countDownToShoot = countDownReset;
            if (nearestEnemyInsideCollider != null) ;
            Shoot(nearestEnemyInsideCollider);
        }
    }

    public Enemy GetNearestEnemyInsideCollider()
    {
        if (enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() > 1)
        {
            enemyManager.SortlistOfEnemiesInsideTheTowerCollider();
            return enemyManager.GetIEnemyFromColliderList(0);
        }
        if (enemyManager.GetAmmountOflistOfEnemiesInsideTheTowerCollider() == 1)
        {
            Enemy IEnemy = enemyManager.GetIEnemyFromColliderList(0);
            return IEnemy;
        }
        else
            return null;
    }

    public Vector3 GetTowerPosition()
    {
        return transform.position;
    }

    public void Shoot(Enemy objetive)
    {
        if (objetive != null)
        {
            fireBallManager.ShootProjectile(objetive);
            flashShootEffect.SetActive(false);
            flashShootEffect.SetActive(true);
        }
    }

    public void ReciveDamage(float damage)
    {
        life -= damage;
    }

    // Método para lanzar repetidamente un objeto explosivo y realizar una explosión al colisionar
    public void ThrowExplosiveMines()
    {
        if (firstActivation)
        {
            firstActivation = false;
            StartCoroutine(ThrowMinesCoroutine(ammountOfMines, secondsBetweenMines));
            effectMinesDeployButton.SetActive(true);
            EndActionOfThrowExplosiveMines();
        }
    }

    public void ThrowFinalAtack()
    {
        MainCamera.instance.TurnOnWinDragonCamera();
        efectSparks.SetActive(true);
        MainMenu.Instance.SetFinalAtackTrue();
        IncreseCoreSize();
        effectFinalAtackButton.SetActive(true);
    }

    public async Task IncreseCoreSize() 
    {
        await Task.Delay(1000);
        MainMenu.Instance.TurnOffbuttonFinalAtackInGame();
        await Task.Delay(500);
        effectFinalAtackButton.SetActive(false);
        midCore.SetActive(true);
        await Task.Delay(500);
        basicCore.SetActive(false);
        await Task.Delay(500);
        bigCore.SetActive(true); 
        await Task.Delay(500);
        midCore.SetActive(false);
        await Task.Delay(500);
        finalBigCore.SetActive(true);
        await Task.Delay(500);
        bigCore.SetActive(false);
        await Task.Delay(500);
    }

    public async Task EndActionOfThrowExplosiveMines() 
    {
        await Task.Delay(1000);
        minesDeployButton.SetActive(false);
        await Task.Delay(1000);
        MainMenu.Instance.TurnOnShootButton();
        effectMinesDeployButton.SetActive(false);
    }

    private IEnumerator ThrowMinesCoroutine(int count, float delayBetweenThrows)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delayBetweenThrows);
            Vector3 launchPosition = GetRandomPointBetweenCorners();
            ExplosiveMine explosiveMine = Instantiate(explosiveMinePrefabB, launchPosition, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPointBetweenCorners()
    {
        // Obtén las posiciones de los cuatro corners en el espacio global
        Vector3 cornerAPosition = CornerA.TransformPoint(Vector3.zero);
        Vector3 cornerBPosition = CornerB.TransformPoint(Vector3.zero);
        Vector3 cornerCPosition = CornerC.TransformPoint(Vector3.zero);
        Vector3 cornerDPosition = CornerD.TransformPoint(Vector3.zero);

        // Encuentra los valores mínimos y máximos en las coordenadas X, Y y Z
        float minX = Mathf.Min(cornerAPosition.x, cornerBPosition.x, cornerCPosition.x, cornerDPosition.x);
        float maxX = Mathf.Max(cornerAPosition.x, cornerBPosition.x, cornerCPosition.x, cornerDPosition.x);
        float minY = Mathf.Min(cornerAPosition.y, cornerBPosition.y, cornerCPosition.y, cornerDPosition.y);
        float maxY = Mathf.Max(cornerAPosition.y, cornerBPosition.y, cornerCPosition.y, cornerDPosition.y);
        float minZ = Mathf.Min(cornerAPosition.z, cornerBPosition.z, cornerCPosition.z, cornerDPosition.z);
        float maxZ = Mathf.Max(cornerAPosition.z, cornerBPosition.z, cornerCPosition.z, cornerDPosition.z);

        // Genera puntos aleatorios dentro del rango de coordenadas X, Y y Z
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        // Crea un vector con las coordenadas aleatorias
        Vector3 randomPoint = new Vector3(randomX, randomY, randomZ);

        return randomPoint;
    }
}
