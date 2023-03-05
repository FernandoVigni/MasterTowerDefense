using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetIncomeValue(10);
        SetLife(80);
        SetSpeed(15);
        SetMagicArmor(10);
    }
}
