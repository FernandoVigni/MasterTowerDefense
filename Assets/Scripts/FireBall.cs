using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    public int fireBallDamage;
    public float fireBallSpeed;

    private void Start()
    {
        SetSpeed(40);
        SetDamage(80);
    }

    public int ExplosiveContact() 
    {
        return 0;    
    }

    public void OnDeath() 
    {
        
    }
}