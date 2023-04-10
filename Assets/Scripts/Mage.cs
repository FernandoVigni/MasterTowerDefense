using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Mage : Enemy
{
    void Start()
    {
        SetGoldValue(800000);
        SetLife(80);
        SetSpeed(15);
        SetMagicDamage(0);
        SetPhysicalDamage(100);
        SetMagicArmor(10);
        recalculateWithTheCoefficient(coefficient);
    }

    public async void SuperCharge() 
    {
        await Task.Delay(2500);
        // animacion de cargando energia
        magicDamage *= 3;
    }

    public void EnergyBall() 
    {
        tower.ReciveDamage(magicDamage);
    }
}
