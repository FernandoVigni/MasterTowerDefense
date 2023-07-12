using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float speed; // Velocidad de movimiento del dragón
    public float rotationSpeed; // Velocidad de rotación del dragón
    public int currentwayPoint;
    public float objetiveDistance;
    public float actualDistance;
    public Vector3 direction;
    public Vector3[] wayPoints;
    Quaternion targetRotation;

    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform point5;
    public Transform point6;
    public Transform point7;
    public Transform point8;

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
            new Vector3(point8.position.x,point8.position.y,point8.position.z)
        };

        currentwayPoint = 1; 
    }

    void Update()
    {
        if (currentwayPoint < wayPoints.Length)
        {
            SetDirection(wayPoints[currentwayPoint]);
            CalculateTargetRotation();
            ApplyGradualRotationTowardsTarget();
            MoveDragonForwardInCurrentDirection();
            actualDistance = Vector3.Distance(transform.position, wayPoints[currentwayPoint]);
            // Comprobar si se ha alcanzado el waypoint actual
            if (actualDistance < objetiveDistance)
            {
                currentwayPoint++; // Pasar al siguiente waypoint
            }
        }
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
