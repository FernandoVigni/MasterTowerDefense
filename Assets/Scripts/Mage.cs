using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Mage : Enemy
{
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
