using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class GameExample : MonoBehaviour
    {
        HealthManager healthManager;
        UIObserver uiObserver;
        GameplayObserver gameplayObserver;
        private void Start()
        {
             healthManager = new HealthManager();
             uiObserver = new UIObserver();
             gameplayObserver = new GameplayObserver();

            // Register observers with the HealthManager
            healthManager.OnHealthChanged += uiObserver.OnHealthChanged;
            healthManager.OnHealthChanged += gameplayObserver.OnHealthChanged;

            // Simulate damage to the character
            healthManager.TakeDamage(20);
        }

        private void OnDisable()
        {
            // UnRegister observers with the HealthManager
            healthManager.OnHealthChanged -= uiObserver.OnHealthChanged;
            healthManager.OnHealthChanged -= gameplayObserver.OnHealthChanged;
        }

    }
}