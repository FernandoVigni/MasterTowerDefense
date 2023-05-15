using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    void Start()
    {
        SetGoldValue(3);
        SetLife(500);
        SetSpeed(5);
        SetMagicDamage(0);
        SetPhysicalDamage(200);
        SetMagicArmor(10);
        recalculateStatsWithTheCoefficient(coefficient);

        SetGoldValue(3);
        SetLife(300);
        SetSpeed(10);

        SetMagicDamage(0);
        SetMagicArmor(10);

        SetPhysicalDamage(75);
        SetPhysicalArmor(10);

        recalculateStatsWithTheCoefficient(coefficient);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream()
    {
        MagicArmorIncreased();
    }

    public void MagicArmorIncreased() 
    {
        magicArmor *= 1.3f;
    }
}
