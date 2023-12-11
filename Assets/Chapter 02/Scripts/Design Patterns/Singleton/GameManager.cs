using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern
{
    public class GameManager : MonoBehaviour
    {
        // Static reference to the instance
        private static GameManager _instance;

        // Public property to access the instance
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // If the instance is null, create a new instance
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        // Other GameManager properties and methods
        public void StartGame()
        {
            Debug.Log("Game Started!");
        }

    }
}
