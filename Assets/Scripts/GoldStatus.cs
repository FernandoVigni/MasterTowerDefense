using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldStatus : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textMesh;
    public float currentGold;
    private const string variableGold = "Gold";

    private void Update()
    {
        currentGold = GetGoldAmmount();
        textMesh.text = currentGold.ToString();
    }

    public void RecibeGold(float goldToIncrese)
    {
        currentGold += goldToIncrese;
        PlayerPrefs.SetFloat(variableGold, currentGold);
        PlayerPrefs.Save();
    }

    private float GetGoldAmmount()
    {
        return PlayerPrefs.GetFloat(variableGold, 0f);
    }
}
