
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    private void Start()
    {
        SetIncomeValue(15);
        SetLife(80);
        SetSpeed(15);
        SetMagicArmor(10);
    }



}
