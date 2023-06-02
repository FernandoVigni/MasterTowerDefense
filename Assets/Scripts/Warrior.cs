using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Warrior : Enemy
{
    private void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
       //      Animator anim = GetComponentInChildren<Animator>(); ;
        //anim.SetBool("Walk", true);
    }

    public async void Roar()
    {
        isWalking = false;
     //   animator.Play("IdleAction");
        //animator . hacer mas grande 
        await Task.Delay(1000);
        physicalDamage *= 2;

        transform.localScale = new Vector3(3f, 3f, 3f);
            isWalking = true;
      //  animator.Play("Walk");
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
