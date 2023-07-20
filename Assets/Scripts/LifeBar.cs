using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image lifeBar;
    public float currentLife;
    public float maxLife;

    public void SetCurrentLife(float currentLife) 
    {
        currentLife = this.currentLife;
        lifeBar.fillAmount = currentLife / maxLife;
    }

    public void SetMaxLife(float maxLife) 
    {
        maxLife = this.maxLife;
    }
}
