using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Warrior : Enemy
{
    private void Start()
    {
        SetIncomeValue(10);
        SetLife(80);
        SetSpeed(15);
        SetMagicArmor(10);
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
        int AxAttackDamage = physicalDamage * 2;
        tower.ReciveDamage(AxAttackDamage);
        await Task.Delay(1000);
    }
}
