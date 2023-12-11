using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern
{
    public class PlayerController : MonoBehaviour
    {
        private void Start()
        {
            // Accessing the GameManager instance
            GameManager.Instance.StartGame();
        }

    }
}