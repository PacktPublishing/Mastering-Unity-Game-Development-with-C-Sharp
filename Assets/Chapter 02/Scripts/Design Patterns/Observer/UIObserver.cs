using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class UIObserver : MonoBehaviour , IHealthObserver
    {
        public void OnHealthChanged(int health)
        {
            // Update UI elements based on the received health value
            Debug.Log($"Health UI Updated: {health}");
            // Additional UI update logic...
        }

    }
}