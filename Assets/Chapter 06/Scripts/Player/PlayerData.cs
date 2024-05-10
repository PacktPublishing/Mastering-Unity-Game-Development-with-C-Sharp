using UnityEngine;

namespace Chapter6
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public string playerName;
        public int playerLevel;
        public float playerExperience;
    }

}