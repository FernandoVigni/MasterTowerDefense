using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private const float TARGET_ASPECT_RATIO = 9f / 16f; // Relación de aspecto objetivo (en este ejemplo, 9:16)
    private const float TARGET_HEIGHT = 10f; // Altura de la cámara para la relación de aspecto objetivo

    public Tower tower;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        SetCameraSize();
    }

    private void Update()
    {
        transform.LookAt(tower.transform.position);
    }

    void SetCameraSize()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height; // Relación de aspecto actual de la pantalla
        float currentHeight = cam.orthographicSize * 2f; // Altura actual de la cámara
        float scaleHeight = TARGET_HEIGHT / currentHeight; // Escala para ajustar la altura de la cámara

        if (currentAspectRatio < TARGET_ASPECT_RATIO)
        {
            float scaleWidth = currentAspectRatio / TARGET_ASPECT_RATIO; // Escala para ajustar el ancho de la cámara
            cam.orthographicSize *= Mathf.Max(scaleWidth, scaleHeight); // Aplicar la escala máxima a la altura o ancho de la cámara
        }
        else
        {
            cam.orthographicSize *= scaleHeight; // Aplicar la escala a la altura de la cámara
        }
    }
}
