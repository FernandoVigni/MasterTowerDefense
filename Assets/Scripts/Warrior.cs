using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Warrior : Enemy
{
    private void Start()
    {
        SetGoldValue(10);
        SetLife(100);
        SetSpeed(15);
        SetMagicDamage(150);
        SetPhysicalDamage(0);
        SetMagicArmor(10);
        recalculateWithTheCoefficient(coefficient);
    }

    public async void Roar()
    {
        isWalking = false;
        await Task.Delay(1000);
        physicalDamage *= 2;
        isWalking = true;
    }

    public async void AxAttack()
    {
        float AxAttackDamage = physicalDamage * 2;
        tower.ReciveDamage(AxAttackDamage);
        await Task.Delay(1000);
    }
}
