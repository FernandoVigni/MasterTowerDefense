using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    public float gold;

    public void RecibeGold(float gold) 
    {
        this.gold += gold;
    }

    public void ReciveBagOfGold(float bagOfGold)
    {
       // gold += bagOfGold;
    }
}
