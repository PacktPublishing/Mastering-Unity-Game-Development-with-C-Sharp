using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FusionFuryGame
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] float startingMaxHealth = 100;  // Set a default starting maximum health for the Enemy
        private float maxHealth;
        private float currentHealth;

        [SerializeField] float healAmount = 5f;    // Amount of healing per interval

        [SerializeField] float healInterval = 5f;  // Time interval for healing

        private WaitForSeconds healIntervalWait;  // Reusable WaitForSeconds instance
        private Coroutine healOverTimeCoroutine;

        public UnityAction onEnemyDied = delegate { };
        public float MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                Debug.Log("Set Health " + currentHealth);
                if (currentHealth <= 0)
                {
                    Debug.Log("OnEnemy Died ");
                    onEnemyDied.Invoke();
                }
            }
        }
        private void Start()
        {
            SetMaxHealth();  // Set initial max health
            healIntervalWait = new WaitForSeconds(healInterval);
            StartHealingOverTime();
        }

        public void SetMaxHealth()
        {
            MaxHealth = startingMaxHealth;
            currentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Taking Damage: " + damage);
            // Implement logic to handle taking damage
            CurrentHealth -= damage;
        }

        //we can also just heal in some states only 
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
