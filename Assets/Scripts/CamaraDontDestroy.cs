using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDontDestroy : MonoBehaviour
{
    private static CamaraDontDestroy instance;
    [SerializeField] private Camera renderCamera;

    private void Awake()
    {
        if (instance == null)
        {
            // Si no hay ninguna instancia existente, establece esta como la instancia actual
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye esta cámara para evitar duplicados
            Destroy(gameObject);
        }
    }

     private void Start()
    {
        // Asignar la cámara al componente Render Camera
        if (renderCamera != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = renderCamera;
        }
    }
}
