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
    private const string variableIsFirstProphecy = "isFirstProphecy";
    private const string variableFirstTimeGame = "firstTimeGame";

    public float powerUpUpdated;
    public float minesDeployUpdate;
    public float hyperBeamUpdate;
    public float shotSpeedAtackUpdate;
    public float isFirstProphecy;
    public float firstTimeGame;

    /* Reset de botones para pruebas */

    public void SetInitialsValues() 
    {
        PlayerPrefs.SetFloat(variableGold, 0);
        PlayerPrefs.SetFloat(variablePowerUp, 0f);
        PlayerPrefs.SetFloat(variableMinesDeploy, 0f);
        PlayerPrefs.SetFloat(variableHyperBeam, 0f);
        PlayerPrefs.SetFloat(variableManualShotSpeedAtack, 1.5f);
        PlayerPrefs.SetFloat(variableIsFirstProphecy, 0f);
        PlayerPrefs.SetFloat(variableFirstTimeGame, 0f);
        PlayerPrefs.Save(); 
    }

    private void Start()
    {
        float firstGameFloat = GetIsFirstGame();
        firstGameFloat = 0;

        if (firstGameFloat == 0) 
        {
            SetInitialsValues();
        }
    }

    public float GetIsFirstGame()
    {
        float firstTimeGame = PlayerPrefs.GetFloat(variableFirstTimeGame);
        return firstTimeGame;
    }

    private void Update()
    {
        currentGold = GetGoldAmmount();
        powerUpUpdated = GetPowerUpValue();
        minesDeployUpdate = GetMinesDeployValue();
        hyperBeamUpdate = GetHyperBeamValue();
        isFirstProphecy = GetIsFirstProphecyTrue();
        firstTimeGame = GetIsFirstGame();
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

    public void SetIsFirstProphecyTrue() 
    {
        PlayerPrefs.SetFloat(variableIsFirstProphecy, 1f);
        PlayerPrefs.Save();
    }

    public void SetIsFirstGameTrue()
    {
        PlayerPrefs.SetFloat(variableFirstTimeGame, 1f);
        PlayerPrefs.Save();
    }

    public float GetIsFirstProphecyTrue()
    {
        float isFirstProphecy = PlayerPrefs.GetFloat(variableIsFirstProphecy);
        return isFirstProphecy;
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
        return shotSpeedAtackUpdate;
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
