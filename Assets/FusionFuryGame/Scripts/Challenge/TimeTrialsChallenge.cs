using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class TimeTrialsChallenge : BaseChallenge
    {
        public float timeLimitInSeconds = 180f;  // Adjust as needed

        public override void StartChallenge()
        {
            if (!commonData.isCompleted)
            {
                StartCoroutine(StartTimeTrialsChallenge());
            }
            else
            {
                Debug.Log("Challenge already completed!");
            }
        }

        IEnumerator StartTimeTrialsChallenge()
        {
            float timer = 0f;

            while (timer < timeLimitInSeconds)
            {
                // Implement your time trials logic here
                // Update the timer and handle UI display or other feedback to the player
                timer += Time.deltaTime;

                yield return null;  // This will make the loop wait for the next frame
            }
            CompleteChallenge();
        }

        public override void CompleteChallenge()
        {
            if (!commonData.isCompleted)
            {
                RewardManager.Instance.GrantReward(commonData);
                commonData.isCompleted = true;
                SaveManager.SaveData(challengeSavedKey, JsonUtility.ToJson(commonData));

            }
            else
            {
                Debug.Log("Challenge already completed!");
            }
        }
    }
}