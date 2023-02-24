using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    public int fireBallDamage;
    public float fireBallSpeed;

    private void Start()
    {
        this.damage = fireBallDamage;
        this.speed = fireBallSpeed;
    }

    public int ExplosiveContact() 
    {
        return 0;    
    }
}