using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    void Start()
    {
        SetGoldValue(100);
        SetLife(500);
        SetSpeed(5);
        SetMagicArmor(10);
    }
}
