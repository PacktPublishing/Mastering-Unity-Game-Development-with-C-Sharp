using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNumber = 1;


    private void OnEnable()
    {
        GameAnalytics.onInitialize += GameAnalytics_onInitialize;
    }

    private void GameAnalytics_onInitialize(object sender, bool e)
    {
        Debug.Log("Game Analytics initialized " + GameAnalytics.Initialized);

        
    }

    private void OnDisable()
    {
        GameAnalytics.onInitialize -= GameAnalytics_onInitialize;
    }
    private void Start()
    {
        GameAnalytics.Initialize();
        
    }
    [ContextMenu("LevelCompleted")]
    // Call this method when the player completes a level
    public void LevelCompleted()
    {
        levelNumber++;
        // Track the event using GameAnalytics
        GameAnalytics.NewDesignEvent("LevelComplete", levelNumber);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "AD");
    }
}
