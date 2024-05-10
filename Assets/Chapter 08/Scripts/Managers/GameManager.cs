using UnityEngine;
using GameAnalyticsSDK;

namespace Chapter8
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }

        public void LevelCompleted(int levelNum)
        {
            // Track the event using GameAnalytics 
            GameAnalytics.NewDesignEvent("LevelComplete", levelNum);
        }
    }

}