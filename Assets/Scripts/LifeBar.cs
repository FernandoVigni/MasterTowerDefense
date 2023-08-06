using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Transform mainCameraTransform;
    private Slider slider;
    public float lifePercentage;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        slider = GetComponent<Slider>();  
    }

    public void SetRemainingLifeToShow(float currentLife, float maxLife)
    {
        lifePercentage = currentLife / maxLife;
        slider.value = lifePercentage;
        //aplicar el % current para mostrar.
    }

    private void LateUpdate()
    {
        // Rotar la life bar para que siempre mire hacia la cámara
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
                         mainCameraTransform.rotation * Vector3.up);
    }
}
