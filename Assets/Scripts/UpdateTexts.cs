using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTexts : MonoBehaviour
{
    public GoldStatus goldStatus;
    public TextMeshProUGUI textMeshColor;
    public TextMeshProUGUI textMeshBlack;

    private void Update()
    {
        textMeshColor.text = goldStatus.currentGold.ToString();
        textMeshBlack.text = goldStatus.currentGold.ToString();
    }
}
