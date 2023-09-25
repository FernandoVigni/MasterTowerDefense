using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static HabilityHandler;

public class UpdateButtons : MonoBehaviour
{
    public HabilityHandler habilityHandler;
    public TextMeshProUGUI powerUp;
    public TextMeshProUGUI powerUpBlack;
    public TextMeshProUGUI minesDelpoy;
    public TextMeshProUGUI minesDelpoyBlack;
    public TextMeshProUGUI finalHit;
    public TextMeshProUGUI finalHitBlack;

    public void SetValuesOfHabilities() 
    {
        SetPowerUpValue();
        SetMinesDeployValue();
        SetFinalHit();
    }

    public void SetPowerUpValue() 
    {
        Hability powerUpHability = habilityHandler.HabilityList.Find(h => h.name == "PowerUp");
        if (powerUpHability != null)
        {
            powerUp.text = powerUpHability.cost.ToString();
            powerUpBlack.text = powerUpHability.cost.ToString();
        }
    }

    public void SetMinesDeployValue()
    {
        Hability explosiveMineHability = habilityHandler.HabilityList.Find(h => h.name == "ExplosiveMine");
        if (explosiveMineHability != null)
        {
            minesDelpoy.text = explosiveMineHability.cost.ToString();
            minesDelpoyBlack.text = explosiveMineHability.cost.ToString();
        }
    }

    public void SetFinalHit()
    {
        Hability hyperBeamHability = habilityHandler.HabilityList.Find(h => h.name == "HyperBeam");
        if (hyperBeamHability != null)
        {
            finalHit.text = hyperBeamHability.cost.ToString();
            finalHitBlack.text = hyperBeamHability.cost.ToString();
        }
    }
}
