using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Mage : Enemy
{
    void Start()
    {
        SetLife(80);
        SetSpeed(15);
    }

    public async void SuperCharge() 
    {
        await Task.Delay(2500);
        // animacion de cargando energia
        magicDamage *= 3;
    }

    public void EnergyBall() 
    {
        enemyManager.tower.ReciveDamage(magicDamage);
    }
}
