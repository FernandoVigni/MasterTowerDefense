
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    private void Start()
    {
        SetIncomeValue(15);
        SetLife(50);
        SetSpeed(20);
        SetMagicArmor(10);
    }
}
