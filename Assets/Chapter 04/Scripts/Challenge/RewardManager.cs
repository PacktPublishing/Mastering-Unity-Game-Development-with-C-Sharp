using UnityEngine;


namespace Chapter4
{
    public class RewardManager : Singlton<RewardManager>
    {


        public void GrantReward(CommonChallengeData commonData)
        {
            // Add code here to handle the specific reward type 
            switch (commonData.rewardType)
            {
                case RewardType.PowerUp:
                    // Grant temporary power-up 
                    break;
                case RewardType.UnlockableWeapon:
                   // Unlock a new weapon 
                    break;
                case RewardType.ScoreMultiplier:
                    ApplyScoreMultiplier(commonData.rewardAmount);
                    break;
                case RewardType.SecretArea:
                    // Grant items found in a secret area 
                   break;
                case RewardType.Coins:
                    GrantCoins(commonData.rewardAmount);
                    break;
            }
        }

        private void ApplyScoreMultiplier(int multiplier)
        {
            ScoreManager.Instance.ApplyMultiplier(multiplier);
            Debug.Log($"Score Multiplier Applied: {multiplier}x");
        }

        private void GrantCoins(int coinAmount)
        {
            CurrencyManager.Instance.AddCoins(coinAmount);
            Debug.Log($"Coins Granted: {coinAmount}");
        }
    }

}