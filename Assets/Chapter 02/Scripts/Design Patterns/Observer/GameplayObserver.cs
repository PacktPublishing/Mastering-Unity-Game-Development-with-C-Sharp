using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class GameplayObserver : MonoBehaviour , IHealthObserver
    {
        public void OnHealthChanged(int health)
        {
            // Update gameplay mechanics based on the received health value
            Debug.Log($"Gameplay Updated: {health}");
            // Additional gameplay update logic...
        }

    }
}