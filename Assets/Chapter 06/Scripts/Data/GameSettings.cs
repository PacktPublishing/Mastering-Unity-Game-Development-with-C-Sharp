using System.Collections;
using UnityEngine;

namespace Chapter6
{
    [CreateAssetMenu(fileName = "NewSettings", menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public int playerHealth;
        public int enemyCount;
        public float playerSpeed;
    }

}