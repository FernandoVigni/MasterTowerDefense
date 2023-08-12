using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonAnimation : MonoBehaviour
{
    public GameObject homeCircle;
    public float rotationSpeed = 30f; // Velocidad de rotación en grados por segundo

    private void Update()
    {
        // Rotar el objeto homeCircle en el eje Z con una velocidad constante
        homeCircle.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
