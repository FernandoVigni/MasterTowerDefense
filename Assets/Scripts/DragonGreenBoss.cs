using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGreenBoss : Enemy
{
    private void Start()
    {
        MoveToTower();
    }

    public void MoveToTower() 
    {
        LookTower();
        StartMove();
    }
}
