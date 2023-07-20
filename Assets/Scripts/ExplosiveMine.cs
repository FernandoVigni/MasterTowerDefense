using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    public float downwardForce; // Reduce the downward force value to make it fall more slowly
    private Rigidbody rb;
    private bool hasCollided = false; // Flag to track collision
    private Vector3 collisionPosition; // Variable to store the collision position
    public GameObject explosion;
    public GameObject auraBomb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            auraBomb.SetActive(true);
            hasCollided = true; // Set the flag to true when colliding with the object tagged "Ground"
            collisionPosition = transform.position; // Store the position at the moment of collision
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode(); // Call the Explode method when colliding with the object tagged "Enemy"
        }
    }

    private void FixedUpdate()
    {
        if (!hasCollided)
        {
            // Continue applying downward force until collision occurs
            Vector3 downwardVector = new Vector3(0, -downwardForce, 0);
            rb.AddForce(downwardVector, ForceMode.Force);
        }
        else
        {
            // Stop applying force and set the position to the collision position
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = collisionPosition;
        }
    }

    private void Explode()
    {
        explosion.SetActive(true);
        Debug.Log("Explode!");
    }
}
