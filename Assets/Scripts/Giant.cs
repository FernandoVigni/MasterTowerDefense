using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    void Start()
    {
        SetGoldValue(3);
        SetLife(300);
        SetSpeed(10);
        SetAtackSpeed(30);

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
