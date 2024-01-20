using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FusionFuryGame
{
    public abstract class BaseEnemy : MonoBehaviour, IDamage , IHealth
    {
        private IDamage playerDamage;
        [SerializeField] float damage; //when the enemy collide with the player
        [SerializeField] float fireDamage; //when the enemy shoot the player
        [SerializeField] float startingMaxHealth = 100;  // Set a default starting maximum health for the Enemy
        [SerializeField] float healAmount = 5f;    // Amount of healing per interval

        [SerializeField] float healInterval = 2f;  // Time interval for healing

        private WaitForSeconds healIntervalWait;  // Reusable WaitForSeconds instance
        private Coroutine healOverTimeCoroutine;

        public BaseWeapon attachedWeapon;  // Reference to the attacted Weapon
        public Transform player;
        [HideInInspector] public NavMeshAgent navMeshAgent;

        // Reference to the current state
        protected IEnemyState currentState;

        // Define the different states
        public IEnemyState wanderState;
        public IEnemyState idleState;
        public IEnemyState attackState;
        public IEnemyState deathState;
        public IEnemyState chaseState;

        public float attackRange = 5f;


        private float maxHealth;
        private float currentHealth;
        [SerializeField] internal float chaseSpeed;
        [SerializeField] internal float rotationSpeed;


        private Animator animator;
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
                if (currentHealth <= 0)
                {
                    // Trigger death logic if health reaches zero
                    TransitionToState(deathState);
                }
            }
        }

        protected virtual void Start()
        {
            // Initialize states
            wanderState = new WanderState();
            idleState = new IdleState();
            attackState = new AttackState();
            chaseState = new ChaseState();
            deathState = new DeathState();

            // Set initial state
            currentState = wanderState;

            // Get references
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();    

            SetMaxHealth();  // Set initial max health
            healIntervalWait = new WaitForSeconds(healInterval);
            StartHealingOverTime();
        }

        protected virtual void Update()
        {
            // Update the current state
            currentState.UpdateState(this);
        }

        public void FireProjectile()
        {
            attachedWeapon.Shoot(damage);
        }

        public void StartAttackAnimations()
        {
            animator.SetBool("IsAttacking", true);
        }

        public void StopAttackAnimations()
        {
            animator.SetBool("IsAttacking", false);
        }

        public bool PlayerInSight()
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Create a ray from the enemy's position towards the player
            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            RaycastHit hit;

            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit, distanceToPlayer))
            {
                // Check if the hit object is the player
                if (hit.collider.CompareTag("Player"))
                {
                    // The player is in sight
                    return true;
                }
            }

            // No direct line of sight to the player
            return false;
        }

        public bool PlayerInRange()
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Check if the player is within the attack range
            if (distanceToPlayer <= attackRange)
            {
                // Calculate the angle between the enemy's forward direction and the direction to the player
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);

                // Set a cone angle to define the attack range
                float attackConeAngle = 45f; // Adjust this value based on your game's requirements

                // Check if the player is within the cone angle
                if (angleToPlayer <= attackConeAngle * 0.5f)
                {
                    // The player is in range and within the attack cone
                    return true;
                }
            }

            // Player is not within attack range or cone angle
            return false;
        }

        public bool IsIdleConditionMet()
        {
            return !PlayerInSight() && !PlayerInRange();
        }


        public void TransitionToState(IEnemyState newState)
        {
            currentState?.ExitState(this);
            currentState = newState;
            currentState?.EnterState(this);
        }

        public float GetDamageValue()
        {
            // You can implement more sophisticated logic here based on enemy stats
            return damage; 
        }

        //we can also make layers for them and reduce calculations of collision in layer matrix in project settings 
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                if (collision.gameObject.TryGetComponent(out playerDamage))
                {
                    TakeDamage(playerDamage.GetDamageValue());
                }
            }
        }

        public void TakeDamage(float damage)
        {
            // Implement logic to handle taking damage
            CurrentHealth -= damage;
        }

        public void SetMaxHealth()
        {
            MaxHealth = startingMaxHealth;
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
