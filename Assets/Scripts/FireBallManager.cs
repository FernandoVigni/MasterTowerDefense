using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public FireBall fireBallPrefab;
    public  FireBall firstFireBall;
    public FireBall fireBall;
    public GameObject spawnPoint;
    public int numFireBalls;
    public float speed;
    public  Vector3 positionToInstantiate;
    public List<FireBall> fireBalls = new List<FireBall>();
    public int ammountOFireBalls;

    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            Debug.Log("creada la " + i + "° fire Ball");
            // Creamos una variable para guardar la posición en la que queremos instanciar el objeto
            positionToInstantiate = new Vector3(0, 8f, 0f); // Cambia los valores por las coordenadas que necesites

            FireBall fireBall = Instantiate(fireBallPrefab, positionToInstantiate,  Quaternion.identity);
            //fireBall.SetActive(false);
            fireBall.translate = false;
            fireBalls.Add(fireBall);

            ammountOFireBalls = fireBalls.Count;
            Debug.Log("hay en total " + ammountOFireBalls + " Fire Balls pipipi" );
        }
    }
    public void GoToSpawnPointPosition(FireBall fireBall)
    {
        Vector3 vector3SpawnPoint = spawnPoint.transform.position;
        Debug.Log(vector3SpawnPoint);
        fireBall.transform.position = vector3SpawnPoint;
    }

    public void ShootNewFireball(Vector3 objetive)
    {
        //GoToSpawnPointPosition(fireBall);
        Debug.Log("chose FireBall");
        fireBall = ChooseFirstFireBall();
        Debug.Log("lookAt");
        fireBall.LookAt(objetive);
        Debug.Log("AbleTo Translate");
        fireBall.AbleToTranslate();
    }

    public FireBall ChooseFirstFireBall()
    {
        // Si la lista está vacía, devolvemos null
        if (fireBalls.Count <= 0)
        {
            Debug.Log("la lista de count de fireballs esta en 0!!!");
            return null;
        }

        // Tomamos la primera FireBall de la lista y la quitamos de la misma
        firstFireBall = fireBalls[0];
        Debug.Log("Removemos el FIrst");
        fireBalls.RemoveAt(0);

        // La movemos al final de la lista
        Debug.Log("agregamos el fireball al final");
        fireBalls.Add(firstFireBall);

        // Activamos la FireBall y la devolvemos
        //firstFireBall.SetActive(true);
        return firstFireBall;
    }
}
