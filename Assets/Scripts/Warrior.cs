using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Warrior : Enemy
{
    private void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
    }

    public async void Roar()
    {
        isWalking = false;
        await Task.Delay(1000);
        physicalDamage *= 2;
        transform.localScale = new Vector3(3f, 3f, 3f);
        isWalking = true;
    }

    public override void BossScream()
    {
        //TODO LUCECITAS de explosion de que crecio mucho.
        Roar();
        AxeAttack();
    }

    public void AxeAttack()
    {
        //animator axe atack
        float AxeAttackDamageIncresed = physicalDamage * 2;
        tower.ReciveDamage(AxeAttackDamageIncresed);
    }
}
