using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private void OnEnable()
    {
        GameAnalytics.onInitialize += GameAnalytics_onInitialize;
    }

    private void GameAnalytics_onInitialize(object sender, bool e)
    {
        Debug.Log("Game Analytics State " + GameAnalytics.Initialized);
        GameAnalytics.NewDesignEvent("Game Analytics Initialized ", 5);
    }

    private void OnDisable()
    {
        GameAnalytics.onInitialize -= GameAnalytics_onInitialize;

    }
    private void Start()
    {
        GameAnalytics.Initialize();
    }

    // Call this method when the player completes a level
    public void LevelCompleted(int levelNum)
    {
        // Track the event using GameAnalytics
        GameAnalytics.NewDesignEvent("LevelComplete", levelNum);
    }
}
