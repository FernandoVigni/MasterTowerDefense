using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Runner : Enemy
{
    private Animator animator;

    void Start()
    {
        //recalculateStatsWithTheCoefficient(coefficient);
        //anim.SetFloat("Walk", 2f);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream()
    {
        SpeedIncreased();
    }

    public void SpeedIncreased()
    {
        speed *= 1.3f;
    }

    public async Task Scream() 
    {
        animator.SetBool("screaming", true);
        await Task.Delay(2000);
        animator.SetBool("screaming", false);
        Run();
    }

    public void Run() 
    {
        animator.SetBool("run", true);
    }
}
