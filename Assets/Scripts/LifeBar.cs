using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image lifeBar;
    public float maxLife;

    public void SetRemainingLifeToShow(float currentLife) 
    {
        float lifePerCent = currentLife / maxLife;
        lifePerCent = -(lifePerCent - 100);
        lifeBar.fillAmount = lifePerCent;
    }

    public void SetMaxLife(float maxLife) 
    {
        this.maxLife = maxLife;
    }
}
