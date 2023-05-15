using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGreenBoss : Enemy
{
    public bool Scream;

    private void Start()
    {
        MoveToTower();
    }

    private void Update()
    {
        if(distanceToTower <= 100 && Scream == true)
        {
            Scream= false;
            Screm();
        }
    }

    public void MoveToTower()
    {
        LookTower();
        StartMove();
    }

    public void Screm()
    {
       // Enemy[] enemies = FindObjectOfType<Enemy>();
        
    }
}
