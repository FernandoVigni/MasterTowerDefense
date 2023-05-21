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
        gold = GameHandlerBetweenScenes.Instance.coinCount;
        textMesh.text = GameHandlerBetweenScenes.Instance.coinCount.ToString();
    }
}
