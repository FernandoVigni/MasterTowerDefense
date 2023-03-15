using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Mage : Enemy
{
    void Start()
    {
        SetGoldValue(10);
        SetLife(80);
        SetSpeed(15);
        SetMagicArmor(10);
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
