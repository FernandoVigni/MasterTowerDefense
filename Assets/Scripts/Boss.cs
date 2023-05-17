using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool canRoar;
    private void Start()
    {
        SetGoldValue(15);
        SetLife(1000);
        SetSpeed(10);
        SetAtackSpeed(10);

        SetMagicDamage(300);
        SetMagicArmor(10);

        SetPhysicalDamage(300);
        SetPhysicalArmor(10);

        recalculateStatsWithTheCoefficient(coefficient);
    }

    public bool Roar() 
    {
        if (canRoar)
        {
            canRoar = false;
            return true;
        }
        return false;
    }
}
