using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private const float TARGET_ASPECT_RATIO = 9f / 16f; // Relaci�n de aspecto objetivo (en este ejemplo, 9:16)
    private const float TARGET_HEIGHT = 10f; // Altura de la c�mara para la relaci�n de aspecto objetivo

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
        float currentAspectRatio = (float)Screen.width / Screen.height; // Relaci�n de aspecto actual de la pantalla
        float currentHeight = cam.orthographicSize * 2f; // Altura actual de la c�mara
        float scaleHeight = TARGET_HEIGHT / currentHeight; // Escala para ajustar la altura de la c�mara

        if (currentAspectRatio < TARGET_ASPECT_RATIO)
        {
            float scaleWidth = currentAspectRatio / TARGET_ASPECT_RATIO; // Escala para ajustar el ancho de la c�mara
            cam.orthographicSize *= Mathf.Max(scaleWidth, scaleHeight); // Aplicar la escala m�xima a la altura o ancho de la c�mara
        }
        else
        {
            cam.orthographicSize *= scaleHeight; // Aplicar la escala a la altura de la c�mara
        }
    }
}
