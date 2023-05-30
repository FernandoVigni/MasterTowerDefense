using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
     private void Start()
    {
        SetSpeed(60);
        SetDamage(75);
    }

    public int ExplosiveContact() 
    {
        return 0;    
    }

    public void OnDeath() 
    {
        
    }
}