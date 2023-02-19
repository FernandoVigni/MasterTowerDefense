using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public int numFireBalls = 15;

    public List<GameObject> fireBalls = new List<GameObject>();
    public int cantidaddeFireballs;
    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            Debug.Log("creada la " + i + "fire Ball");
            GameObject fireBall = Instantiate(fireBallPrefab);
            //fireBall.SetActive(false);
            fireBalls.Add(fireBall);
            cantidaddeFireballs = fireBalls.Count;
            Debug.Log("hay en total " + cantidaddeFireballs + " Fire Balls" );
        }
    }

    public GameObject ChooseFirstFireBall()
    {
        Debug.Log("la lista tiene antes de elegir la 1ra: " + cantidaddeFireballs );
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
