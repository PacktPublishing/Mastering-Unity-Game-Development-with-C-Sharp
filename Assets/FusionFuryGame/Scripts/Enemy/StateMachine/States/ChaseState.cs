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
            
            if (enemy.PlayerInRange())
            {
                enemy.TransitionToState(enemy.attackState);
            }
            else if(!enemy.IsIdleConditionMet())
            {
                enemy.TransitionToState(enemy.idleState);
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            StopChaseBehavior(enemy);
        }

        private void StartChaseBehavior(BaseEnemy enemy)
        {
            if (enemy.player == null) return;

            enemy.navMeshAgent.SetDestination(enemy.player.position);
        }

        private void ChasePlayer(BaseEnemy enemy)
        {
            LookAtPlayer(enemy);
            enemy.navMeshAgent.speed = enemy.chaseSpeed;
        }

        private void StopChaseBehavior(BaseEnemy enemy)
        {
            enemy.navMeshAgent.ResetPath();
        }

        private void LookAtPlayer(BaseEnemy enemy)
        {
            if (enemy.player == null) return ;

            // Example: Make the enemy face the player while chasing
            Vector3 lookDirection = enemy.player.position - enemy.transform.position;
            lookDirection.y = 0;  // Keep the enemy's rotation in the horizontal plane
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.rotationSpeed);
        }
    }
}