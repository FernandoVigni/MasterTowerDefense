using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Warrior : Enemy
{
    private void Start()
    {
        SetGoldValue(1);
        SetLife(100);
        SetSpeed(15);

        SetMagicDamage(150);
        SetMagicArmor(10);

        SetPhysicalDamage(0);
        SetPhysicalArmor(10);

        recalculateStatsWithTheCoefficient(coefficient);
    }

    public async void Roar()
    {
        isWalking = false;
        await Task.Delay(1000);
        physicalDamage *= 2;
        isWalking = true;
    }

    public override void BossScream()
    {
        AxeAttack();
    }

    public void AxeAttack()
    {
        float AxeAttackDamageIncresed = physicalDamage * 2;
        tower.ReciveDamage(AxeAttackDamageIncresed);
    }
}
