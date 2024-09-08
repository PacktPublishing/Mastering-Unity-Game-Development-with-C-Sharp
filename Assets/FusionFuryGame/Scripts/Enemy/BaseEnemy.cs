using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace FusionFuryGame
{

    [RequireComponent(typeof(EnemyHealth) , typeof(EnemyAnimations) , typeof(EnemyShoot)) ]
    [RequireComponent(typeof(EnemyCollision))]
    public abstract class BaseEnemy : MonoBehaviour  
    {
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

        [SerializeField] internal float chaseSpeed;
        [SerializeField] internal float rotationSpeed;

        internal EnemyAnimations animationComponent;
        internal EnemyShoot shootComponent;
        internal EnemyHealth healthComponent;

        private void OnEnable()
        {
            // Initialize states
            wanderState = new WanderState();
            idleState = new IdleState();
            attackState = new AttackState();
            chaseState = new ChaseState();
            deathState = new DeathState();

            // Set initial state
            currentState = wanderState;
            healthComponent = GetComponent<EnemyHealth>();


        }
        protected virtual void Start()
        {

            // Get references
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
            animationComponent = GetComponent<EnemyAnimations>();
            shootComponent = GetComponent<EnemyShoot>();
            

            healthComponent.onEnemyDied += OnDied;
        }

        protected virtual void Update()
        {
            // Update the current state
            currentState.UpdateState(this);
        }

        public bool PlayerInSight()
        {
            if (player == null) return false;

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
            if (player == null) return false;
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

        private void OnDied()
        {
            
            // Trigger death logic if health reaches zero
            TransitionToState(deathState);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            healthComponent.onEnemyDied -= OnDied;
        }

    }
}
