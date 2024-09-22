using System.Collections;
using UnityEngine;

namespace FusionFuryGame
{
    public class AttackState : IEnemyState
    {
        public float timeBetweenAttacks = 1f;
        private float lastAttackTime = -Mathf.Infinity;

        private float moveWhileShootingChance = 0.5f;
        private bool shouldMoveWhileShooting;

        public void EnterState(BaseEnemy enemy)
        {
            shouldMoveWhileShooting = Random.value < moveWhileShootingChance;
            enemy.animationComponent.StartAttackAnimations();
        }

        public void UpdateState(BaseEnemy enemy)
        {
            LookAtPlayer(enemy);

            if (shouldMoveWhileShooting && !IsWithinAttackDistance(enemy))
            {
                MoveTowardsPlayer(enemy);
            }
            else
            {
                StopMoving(enemy);
            }

            if (Time.time >= lastAttackTime + timeBetweenAttacks)
            {
                AttackPlayer(enemy);
                lastAttackTime = Time.time;
            }

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            // Transition to ChaseState if player is outside attack range but inside chase range
            if (distanceToPlayer > enemy.attackRange && distanceToPlayer <= enemy.chaseRange)
            {
                enemy.TransitionToState(enemy.chaseState);
            }
            // Transition to WanderState if player is outside chase range
            else if (distanceToPlayer > enemy.chaseRange)
            {
                enemy.TransitionToState(enemy.wanderState);
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            enemy.animationComponent.StopAttackAnimations();
        }

        private void LookAtPlayer(BaseEnemy enemy)
        {
            if (enemy.player == null) return;

            Vector3 lookDirection = enemy.player.position - enemy.transform.position;
            lookDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.enemyData.rotationSpeed);
        }

        private void MoveTowardsPlayer(BaseEnemy enemy)
        {
            if (enemy.navMeshAgent == null) return;
            enemy.navMeshAgent.SetDestination(enemy.player.position);
        }

        private void StopMoving(BaseEnemy enemy)
        {
            if (enemy.navMeshAgent != null && enemy.navMeshAgent.isActiveAndEnabled)
            {
                enemy.navMeshAgent.ResetPath();
            }
        }

        private bool IsWithinAttackDistance(BaseEnemy enemy)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            return distanceToPlayer <= enemy.attackRange;
        }

        private void AttackPlayer(BaseEnemy enemy)
        {
            enemy.AttackPlayer();
        }
    }
}
