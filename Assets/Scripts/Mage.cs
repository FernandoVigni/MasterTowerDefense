using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Mage : Enemy
{
    void Start()
    {
        SetGoldValue(1);
        SetLife(80);
        SetSpeed(15);

        SetMagicDamage(100);
        SetMagicArmor(10);

        SetPhysicalDamage(0);
        SetPhysicalArmor(10);

        recalculateStatsWithTheCoefficient(coefficient);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream() 
    {
        SuperCharge();
    }

    public void SuperCharge()
    {
        // animacion de cargando energia
        magicDamage *= 3;
    }

    public void EnergyBall() 
    {
        tower.ReciveDamage(magicDamage);
    }
}
