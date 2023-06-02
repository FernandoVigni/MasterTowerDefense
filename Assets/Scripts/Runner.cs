using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Enemy
{
    void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
        //anim.SetFloat("Walk", 2f);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream()
    {
        SpeedIncreased();
    }

    public void SpeedIncreased()
    {
        speed *= 1.3f;
    }
}
