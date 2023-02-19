using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public int numFireBalls = 1;

    public List<GameObject> fireBalls = new List<GameObject>();
    public int cantidaddeFireballs;
    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            Debug.Log("creada la " + i + "° fire Ball");
            
            // Creamos una variable para guardar la posición en la que queremos instanciar el objeto
            Vector3 positionToInstantiate = new Vector3(-40f, 0f, -40f); // Cambia los valores por las coordenadas que necesites

            // Creamos una variable para guardar la rotación en la que queremos instanciar el objeto
            Quaternion rotationToInstantiate = Quaternion.identity; // La rotación por defecto es la rotación nula

            GameObject fireBall = Instantiate(fireBallPrefab, positionToInstantiate, rotationToInstantiate);
            //fireBall.SetActive(false);
            fireBall.GetComponent<FireBall>().translate = false;
            fireBalls.Add(fireBall);
            cantidaddeFireballs = fireBalls.Count;
            Debug.Log("hay en total " + cantidaddeFireballs + " Fire Balls pipipi" );
        }
    }

    public GameObject ChooseFirstFireBall()
    {
        Debug.Log("la lista tiene antes de elegir la 1ra fireBall: " + cantidaddeFireballs );
        Debug.Log("entro a seleccionar la 1er fireball");
        // Si la lista está vacía, devolvemos null
        if (fireBalls.Count == 0)
        {
            Debug.Log("la sita de count de fireballs esta en 0!!!");
            return null;
        }

        // Tomamos la primera FireBall de la lista y la quitamos de la misma
        GameObject firstFireBall = fireBalls[0];
        Debug.Log("Removemos el FIrst");
        fireBalls.RemoveAt(0);

        // La movemos al final de la lista
        Debug.Log("agregamos el fireball al final");
       // fireBalls.Add(firstFireBall);

        // Activamos la FireBall y la devolvemos
        firstFireBall.SetActive(true);
        return firstFireBall;
    }
}
