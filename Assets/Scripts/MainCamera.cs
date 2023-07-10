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

    [Header("Phase One")]
    public GameObject cameraBaseEndInChaman;
    public GameObject cameraChamanBaseEndInPortals;
    public GameObject portalsEndInChaman;
    public GameObject cameraChamanEndInTowerLeft;


    [Header("Phase Two")]
    public GameObject towerLeft;
    public GameObject cameraFrontRunner;
    public GameObject camera3PersonRunner;
    public GameObject cameraChamanAndPortal;


    [Header("Phase Three")]
    public GameObject camera3PersonTowerRight;
    public GameObject cameraNecromancer;

    [Header("Phase Four")]
    public GameObject camera3PersonTowerRightPhaseThree;

    public void TurnOffPhaseOneCameras() 
    {
        cameraBaseEndInChaman.SetActive(false);
        cameraChamanBaseEndInPortals.SetActive(false);
        portalsEndInChaman.SetActive(false);
        cameraChamanEndInTowerLeft.SetActive(false);
    }

    public void TurnOffPhaseTwoCameras()
    {
        towerLeft.SetActive(false);
        cameraFrontRunner.SetActive(false);
        camera3PersonRunner.SetActive(false);
        cameraChamanAndPortal.SetActive(false);
    }

    public void TurnOffPhaseThreeCameras()
    {
        camera3PersonTowerRight.SetActive(false); ;
        cameraNecromancer.SetActive(false); ;
    }

    public void TurnOffPhaseFourCameras()
    { 
    
    }

    public void SetOffCamerasPhaseOne() 
    {
        cameraBaseEndInChaman.SetActive(false);
        cameraChamanBaseEndInPortals.SetActive(false);
        towerLeft.SetActive(false);
    }
    
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

    public void ActivateCamera(GameObject camera)
    {
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
}
