using System;


namespace Chapter4
{
    [Serializable]
    public class CommonChallengeData
    {
        public bool isCompleted;
        public RewardType rewardType; // Type of reward 
        public int rewardAmount;      // Amount or value of the reward 
        //other challenge Data
    }

}