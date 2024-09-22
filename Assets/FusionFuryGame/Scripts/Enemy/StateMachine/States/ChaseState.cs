using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class ChaseState : IEnemyState
    {
        public void EnterState(BaseEnemy enemy)
        {
            StartChaseBehavior(enemy);
        }

        public void UpdateState(BaseEnemy enemy)
        {
            ChasePlayer(enemy);

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            // Transition to AttackState if player is within attack range
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.TransitionToState(enemy.attackState);
            }
            // Transition to WanderState if player is outside chase range
            else if (distanceToPlayer > enemy.chaseRange)
            {
                enemy.TransitionToState(enemy.wanderState);
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            StopChaseBehavior(enemy);
        }

        private void StartChaseBehavior(BaseEnemy enemy)
        {
            if (enemy.player == null) return;
            enemy.navMeshAgent?.SetDestination(enemy.player.position);
        }

        private void ChasePlayer(BaseEnemy enemy)
        {
            LookAtPlayer(enemy);
            enemy.navMeshAgent.speed = enemy.enemyData.chaseSpeed;
        }

        private void StopChaseBehavior(BaseEnemy enemy)
        {
            enemy.navMeshAgent?.ResetPath();
        }

        private void LookAtPlayer(BaseEnemy enemy)
        {
            if (enemy.player == null) return;

            Vector3 lookDirection = enemy.player.position - enemy.transform.position;
            lookDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.enemyData.rotationSpeed);
        }
    }
}