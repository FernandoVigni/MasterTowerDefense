using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public int numFireBalls = 15;

    private List<GameObject> fireBalls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Instanciamos las 15 FireBalls y las agregamos a la lista
        for (int i = 0; i < numFireBalls; i++)
        {
            GameObject fireBall = Instantiate(fireBallPrefab);
            //fireBall.SetActive(false);
            fireBalls.Add(fireBall);
        }
    }

    public GameObject ChooseFirstFireBall()
    {
        // Si la lista está vacía, devolvemos null
        if (fireBalls.Count == 0)
        {
            return null;
        }

        // Tomamos la primera FireBall de la lista y la quitamos de la misma
        GameObject firstFireBall = fireBalls[0];
        fireBalls.RemoveAt(0);

        // La movemos al final de la lista
        fireBalls.Add(firstFireBall);

        // Activamos la FireBall y la devolvemos
        firstFireBall.SetActive(true);
        return firstFireBall;
    }
}
