using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;

public class MainCamera : MonoBehaviour
{
    public static MainCamera instance;
    private CinemachineBrain cinemachineBrain;
    public PlayableDirector timelineDirector;

    public CinemachineVirtualCamera camera360;
    public CinemachineVirtualCamera cameraPortals;
    public CinemachineVirtualCamera cameraChaman;
    public CinemachineVirtualCamera camera3PersonTowerLeft;
    public CinemachineVirtualCamera camera3PersonTowerRight;
    public CinemachineVirtualCamera cameraFrontRunner;
    public CinemachineVirtualCamera camera3PersonRunner;
    public CinemachineVirtualCamera cameraNecromancer;
    public CinemachineVirtualCamera cameraEnemyesUpgrade;

    private void Awake()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private const float TARGET_ASPECT_RATIO = 9f / 16f; // Relación de aspecto objetivo (en este ejemplo, 9:16)
    private const float TARGET_HEIGHT = 10f; // Altura de la cámara para la relación de aspecto objetivo

    public Tower tower;
    private Camera cam;

    void Start()
    {
        //ActivateCamera(camera1);
        cam = GetComponent<Camera>();
        SetCameraSize();
        timelineDirector = GetComponent<PlayableDirector>();
 
    }

    public void ActivateCamera(CinemachineVirtualCamera camera)
    {
        // Desactivar todas las cámaras
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);

        // Activar la cámara deseada
        camera.gameObject.SetActive(true);
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

    //-------------------------------

    public void SetCameraLookingToPortalOne()
    {
        transform.position = new Vector3(-42, 82, -12);
        transform.rotation = Quaternion.Euler(44, 92, 0);
    }

    public void SetCameraLookingToPortalTwo()
    {
        transform.position = new Vector3(-12, 70, 37);
        transform.rotation = Quaternion.Euler(37, 150, -10);
    }
}
