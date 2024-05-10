using UnityEngine;

namespace Chapter5
{
    // ViewModel 
    public class PlayerViewModel : MonoBehaviour
    {
        private PlayerData playerData;
        // Properties for data binding 
        public int PlayerLevel => playerData.playerLevel;
        public int PlayerScore => playerData.playerScore;

        private void Start()
        {
            playerData = new PlayerData();
        }

        public void UpdatePlayerData(int level, int score)
        {
            playerData.playerLevel = level;
            playerData.playerScore = score;
        }
    }
}