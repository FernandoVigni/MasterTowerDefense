using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Mage : Enemy
{
    public GameObject mageMeshObject;
    void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
        //anim.SetFloat("Walk", 1.5f);
    }

    //EJEMPLO POLIMORFISMO
    public override void BossScream() 
    {
        SuperCharge();
    }

    public void SuperCharge()
    {
        // animacion de cargando energia
        magicDamage *= 3;
    }
}
