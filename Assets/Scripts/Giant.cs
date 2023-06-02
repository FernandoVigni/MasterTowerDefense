using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
        //anim.SetFloat("Walk", 1.5f);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream()
    {
        MagicArmorIncreased();
    }

    public void MagicArmorIncreased() 
    {
        magicArmor *= 1.3f;
    }
}
