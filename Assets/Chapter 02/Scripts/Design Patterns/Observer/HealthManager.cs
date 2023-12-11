using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class HealthManager : MonoBehaviour , IHealthSubject
    {
        private int currentHealth;
        public int MaxHealth { get; private set; } = 100;

        // Event to notify observers when health changes
        public event Action<int> OnHealthChanged;

        private void Start()
        {
            currentHealth = MaxHealth;
        }

        // Method to damage the character
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

            // Notify observers about the health change
            OnHealthChanged?.Invoke(currentHealth);

            // Check for death condition
            if (currentHealth == 0)
            {
                Debug.Log("Character has died!");
                // Additional logic for character death...
            }
        }

    }
}