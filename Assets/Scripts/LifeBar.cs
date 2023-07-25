using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image lifeBar;
    public float maxLife;

    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    public void SetRemainingLifeToShow(float currentLife)
    {
        float lifePercentage = currentLife / maxLife;
        lifeBar.fillAmount = lifePercentage;
    }

    public void SetMaxLife(float maxLife)
    {
        this.maxLife = maxLife;
    }

    private void LateUpdate()
    {
        // Rotar la life bar para que siempre mire hacia la cámara
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
                         mainCameraTransform.rotation * Vector3.up);
    }
}
