using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    void Start()
    {
        SetGoldValue(15);
        SetLife(50);
        SetSpeed(20);
        SetMagicArmor(10);
    }
}
