using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float speed; // Velocidad de movimiento del dragón
    public float rotationSpeed; // Velocidad de rotación del dragón
    public int currentWayPoint;
    public float objetiveDistance;
    public float actualDistance;
    public GameObject snowThrower;
    public GameObject snowEfect;
    public Vector3 direction;
    public Vector3[] wayPoints;
    public EnemyManager enemyManager;
    Quaternion targetRotation;
    public bool isFirstRoar;
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform point5;
    public Transform point6;
    public Transform point7;
    public Transform point8;
    public Transform point9;
    public Transform point10;
    public Transform dragonSpawnPoint;

    public bool WayPointClearOne = false;
    public bool WayPointClearTree = false;
    public bool WayPointClearFour = false;
    public bool WayPointClearFive = false;
    public bool WayPointClearSix = false;
    public bool WayPointClearSeven = false;
    public bool WayPointClearTen = false;


    private void Start()
    {
        // Carga los puntos de destino en el array
        wayPoints = new Vector3[]
        {
            new Vector3(point0.position.x,point0.position.y,point0.position.z),
            new Vector3(point1.position.x,point1.position.y,point1.position.z),
            new Vector3(point2.position.x,point2.position.y,point2.position.z),
            new Vector3(point3.position.x,point3.position.y,point3.position.z),
            new Vector3(point4.position.x,point4.position.y,point4.position.z),
            new Vector3(point5.position.x,point5.position.y,point5.position.z),
            new Vector3(point6.position.x,point6.position.y,point6.position.z),
            new Vector3(point7.position.x,point7.position.y,point7.position.z),
            new Vector3(point8.position.x,point8.position.y,point8.position.z),
            new Vector3(point9.position.x,point9.position.y,point9.position.z),
            new Vector3(point10.position.x,point10.position.y,point10.position.z)
        };
        currentWayPoint = 1; 
    }

    void Update()
    {
        if (currentWayPoint < wayPoints.Length)
        {
            SetDirection(wayPoints[currentWayPoint]);
            CalculateTargetRotation();
            ApplyGradualRotationTowardsTarget();
            MoveDragonForwardInCurrentDirection();
            actualDistance = Vector3.Distance(transform.position, wayPoints[currentWayPoint]);
            // Comprobar si se ha alcanzado el waypoint actual
            if (actualDistance < objetiveDistance)
            {
                currentWayPoint++; // Pasar al siguiente waypoint
            }
        }

        if (currentWayPoint == 5 || currentWayPoint == 10)
        {
            snowThrower.SetActive(true);
            snowEfect.SetActive(true);
            AudioManager.Instance.PlaySFX("FlameTrhrower");
        }
        else 
        {
            if (currentWayPoint == 6)
            {
                snowThrower.SetActive(false);
                snowEfect.SetActive(false);
            }
        }
        FlySoundsSelector(); 
    }

    public void FlySoundsSelector() 
    {
        //Roar
        if (currentWayPoint >= 2 && isFirstRoar == true)
        {
            isFirstRoar = false;
            Roar();
        }

        //FlamenThrower
        if (currentWayPoint == 5 && WayPointClearFive != true)
        {
            WayPointClearFive = true;
            snowThrower.SetActive(true);
            snowEfect.SetActive(true);
            AudioManager.Instance.PlaySFX("FlameTrhrower");
        }

        if (currentWayPoint == 10 && WayPointClearTen != true)
        {
            WayPointClearTen = true;
            snowThrower.SetActive(true);
            snowEfect.SetActive(true);
            AudioManager.Instance.PlaySFX("FlameTrhrower");
        }


        // Flutter
        if (currentWayPoint == 1 && WayPointClearOne != true)
        {
            WayPointClearOne = true;
            AudioManager.Instance.PlaySFX("Flutter");
        }

        if (currentWayPoint == 3 && WayPointClearTree != true)
        {
            WayPointClearTree = true;
            AudioManager.Instance.PlaySFX("Flutter");
        }


        if (currentWayPoint == 6 && WayPointClearSix != true)
        {
            WayPointClearFive = true;
            snowThrower.SetActive(false);
            snowEfect.SetActive(false);
        }


        if (currentWayPoint == 7 && WayPointClearSeven != true)
        {
            WayPointClearSeven = true;
            AudioManager.Instance.PlaySFX("Flutter");
        }
    }

    public void Roar() 
    {
        AudioManager.Instance.PlaySFX("DragonRoar");
    }

    public void ResetDragon()
    {
        this.gameObject.transform.position = dragonSpawnPoint.position;
        isFirstRoar = true;
        currentWayPoint = 1;
        snowThrower.SetActive(false);
        snowEfect.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 objective)
    {
        // Calcular la dirección hacia el objetivo
        direction = (objective - transform.position).normalized;
    }

    public void CalculateTargetRotation()
    {
        targetRotation = Quaternion.LookRotation(direction);
        // Perform additional operations or actions based on the calculated rotation
    }

    public void ApplyGradualRotationTowardsTarget()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // Additional logic or functionality related to the gradual rotation
    }

    public void MoveDragonForwardInCurrentDirection()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // Additional operations or actions for the dragon's forward movement
    }
}
