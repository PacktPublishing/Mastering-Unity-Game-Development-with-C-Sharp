using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FusionFuryGame
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]

    public class PlayerData : ScriptableObject
    {
        public string playerName;
        public int playerLevel;
        public float playerHealth;
    }
}