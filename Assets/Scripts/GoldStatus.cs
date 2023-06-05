using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldStatus : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textMesh;
    public float gold;

    private void Update()
    {
        gold = PhaseManager.Instance.coinCount;
        textMesh.text = PhaseManager.Instance.coinCount.ToString();
    }
}
