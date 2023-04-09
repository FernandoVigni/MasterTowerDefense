using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorManager : MonoBehaviour
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
        StartLevel(4);
    }

    public void IncreseMaxLevelAviable() 
    {
        if(maxLevelAviableToPlay < finalLevel)
            maxLevelAviableToPlay += 1;
    }

    public void StartLevel(int level)
    {
        stageManager.SetLevel(level);
    }
}
