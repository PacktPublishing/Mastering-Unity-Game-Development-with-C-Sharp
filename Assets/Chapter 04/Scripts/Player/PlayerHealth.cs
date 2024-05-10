using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter4
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public static UnityAction onPlayerDied = delegate { };
        public float startingMaxHealth = 100;  // Set a default starting maximum health for the player 
        public float healInterval = 2f;  // Time interval for healing 
        public float healAmount = 5f;    // Amount of healing per interval 

        private WaitForSeconds healIntervalWait;  // Reusable WaitForSeconds instance 
        private Coroutine healOverTimeCoroutine;

        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }


        void OnDestroy()
        {
            // Ensure to stop the healing coroutine when the object is destroyed 
            if (healOverTimeCoroutine != null)
                StopCoroutine(healOverTimeCoroutine);
        }

        void Start()
        {
            SetMaxHealth();  // Set initial max health 

            healIntervalWait = new WaitForSeconds(healInterval);

            StartHealingOverTime();
        }

        public void TakeDamage(float damage)
        {
            // Implement logic to handle taking damage 
            CurrentHealth -= damage;

            // Check for death or other actions based on health status 
            if (CurrentHealth <= 0) onPlayerDied.Invoke();
        }

        public void SetMaxHealth()
        {
            MaxHealth = startingMaxHealth;
        }

        public void Heal()
        {
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        }

        private void StartHealingOverTime()
        {
            healOverTimeCoroutine = StartCoroutine(HealOverTime());
        }

        private IEnumerator HealOverTime()
        {
            while (true)
            {
                yield return healIntervalWait;
                Heal();
            }
        }
    }
    }