using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool canRoar;
    private void Start()
    {
        recalculateStatsWithTheCoefficient(coefficient);
    }

    public bool Roar() 
    {
        if (canRoar)
        {
            canRoar = false;
            return true;
        }
        return false;
    }

    public void validateDistanteToRoar() 
    {
        
    }
}
