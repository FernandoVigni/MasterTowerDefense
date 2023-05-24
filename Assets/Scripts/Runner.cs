using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Enemy
{
    void Start()
    {
        SetGoldValue(2);
        SetLife(50);
        SetSpeed(20);
        SetAtackSpeed(30);

        SetMagicDamage(0);
        SetMagicArmor(10);

        SetPhysicalDamage(20);
        SetPhysicalArmor(10);

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
