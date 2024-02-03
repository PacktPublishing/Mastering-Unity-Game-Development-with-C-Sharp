using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FusionFuryGame.RewardManager;

namespace FusionFuryGame
{
    [CreateAssetMenu(fileName = "CommonChallengeData", menuName = "Data/Common Challenge Data")]

    [Serializable]
    public class CommonChallengeData : ScriptableObject
    {
        public bool isCompleted;
        public RewardType rewardType; // Type of reward
        public int rewardAmount;      // Amount or value of the reward
    }
}
