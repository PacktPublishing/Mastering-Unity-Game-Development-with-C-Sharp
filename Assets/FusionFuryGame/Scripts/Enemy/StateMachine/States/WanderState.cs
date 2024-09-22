using Chapter2.LSP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class WanderState : IEnemyState
    {
        private float wanderTime = 5f;
        private float timer;

        public void EnterState(BaseEnemy enemy)
        {
            timer = 0f;
            StartWanderBehavior(enemy);
        }

        public void UpdateState(BaseEnemy enemy)
        {
            timer += Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            // Transition to ChaseState if player enters chase range
            if (distanceToPlayer <= enemy.chaseRange)
            {
                enemy.TransitionToState(enemy.chaseState);
            }

            // Continue wandering if the player is outside the chase range
            if (timer >= wanderTime)
            {
                enemy.TransitionToState(enemy.idleState);
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            StopWanderBehavior(enemy);
        }

        private void StartWanderBehavior(BaseEnemy enemy)
        {
            Vector3 randomPosition = GetRandomPosition(enemy);
            enemy.navMeshAgent?.SetDestination(randomPosition);
        }

        private void StopWanderBehavior(BaseEnemy enemy)
        {
            enemy.navMeshAgent?.ResetPath();
        }

        private Vector3 GetRandomPosition(BaseEnemy enemy)
        {
            float wanderRadius = 10f;
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += enemy.transform.position;
            randomDirection.y = enemy.transform.position.y;
            return randomDirection;
        }
    }
}