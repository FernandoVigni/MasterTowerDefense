using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GoldStatus : MonoBehaviour
{
    public HabilityHandler habilityHandler;
    public float currentGold;
    private const string variableGold = "Gold";
    private const string variablePowerUp = "PowerUp";
    private const string variableMinesDeploy = "MinesDeploy";
    private const string variableHyperBeam = "HyperBeam";
    private const string variableManualShotSpeedAtack = "ManualShotSpeedAtack";

    public float powerUpUpdated;
    public float minesDeployUpdate;
    public float hyperBeamUpdate;
    public float shotSpeedAtackUpdate;

    /* Reset de botones para pruebas */

    public void RestartButtonsValues() 
    {
        //PlayerPrefs.SetFloat(variableGold, 15000);
        PlayerPrefs.SetFloat(variableGold, 150);
        PlayerPrefs.SetFloat(variablePowerUp, 0f);
        PlayerPrefs.SetFloat(variableMinesDeploy, 0f);
        PlayerPrefs.SetFloat(variableHyperBeam, 0f);
        PlayerPrefs.SetFloat(variableManualShotSpeedAtack, 1.5f);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        RestartButtonsValues();
    }
    
    /* ----------------------------- */


    private void Update()
    {
        currentGold = GetGoldAmmount();
        powerUpUpdated = GetPowerUpValue();
        minesDeployUpdate = GetMinesDeployValue();
        hyperBeamUpdate = GetHyperBeamValue();
    }


    public void SetAtackSpeedIncreased() 
    {
        shotSpeedAtackUpdate = 1.8f;
        PlayerPrefs.SetFloat(variableManualShotSpeedAtack, shotSpeedAtackUpdate);
        PlayerPrefs.Save();
    }

    public void RecibeGold(float goldToIncrese)
    {
        currentGold += goldToIncrese;
        PlayerPrefs.SetFloat(variableGold, currentGold);
        PlayerPrefs.Save();
    }

    public void UseGold(float goldTospend)
    {
        currentGold -= goldTospend;
        PlayerPrefs.SetFloat(variableGold, currentGold);
        PlayerPrefs.Save();
    }

    public void SetPowerUpTrue ()
    {
        float costHability0 = habilityHandler.HabilityList[0].cost;
        if (currentGold > costHability0)
        {
            AudioManager.Instance.PlaySFX("HabilityCoinSpended");
            UseGold(costHability0);
            PlayerPrefs.SetFloat(variablePowerUp, 1f);
            PlayerPrefs.Save();
        }
    }

    public void SetMinesDeployTrue()
    {
        float costHability1 = habilityHandler.HabilityList[1].cost;
        if (currentGold >= costHability1)
        {
            AudioManager.Instance.PlaySFX("HabilityCoinSpended");
            UseGold(costHability1);
            PlayerPrefs.SetFloat(variableMinesDeploy, 1f);
            PlayerPrefs.Save();
        }
    }

    public void SetHyperBeamTrue()
    {
        float costHability2 = habilityHandler.HabilityList[2].cost;
        if (currentGold >= costHability2)
        {
            AudioManager.Instance.PlaySFX("HabilityCoinSpended");
            UseGold(costHability2);
            PlayerPrefs.SetFloat(variableHyperBeam, 1f);
            PlayerPrefs.Save();
        }
    }

    public float GetPowerUpValue()
    {
        return PlayerPrefs.GetFloat(variablePowerUp, 0f);
    }

    public float GetVariableManualShotSpeedAtack()
    {
        return PlayerPrefs.GetFloat(variableManualShotSpeedAtack, 0f);
    }

    public float GetMinesDeployValue()
    {
        return PlayerPrefs.GetFloat(variableMinesDeploy, 0f);
    }

    public float GetHyperBeamValue()
    {
        return PlayerPrefs.GetFloat(variableHyperBeam, 0f);
    }

    private float GetGoldAmmount()
    {
        return PlayerPrefs.GetFloat(variableGold, 0f);
    }
}
