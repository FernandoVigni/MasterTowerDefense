using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetIncomeValue(100);
        SetLife(500);
        SetSpeed(5);
        SetMagicArmor(10);
    }
}
