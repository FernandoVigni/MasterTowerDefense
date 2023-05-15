using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    StageManager stageManager;
    int maxLevelAviableToPlay;
    int finalLevel;

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void Start()
    {
        StartPhase(1);
    }

    public void IncreseMaxLevelAviable() 
    {
        if(maxLevelAviableToPlay < finalLevel)
            maxLevelAviableToPlay += 1;
    }

    public void StartPhase(int phase)
    {
        stageManager.SetCoefficientAndBagOfGold(phase);
    }
}
