using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class AttackState : IEnemyState
    {
        private float attackTimer;  // Timer to control the attack rate
        private float timeBetweenAttacks = 1.5f;  // Adjust as needed based on your game's requirements

        public void EnterState(BaseEnemy enemy)
        {
            enemy.StartAttackAnimations();
            attackTimer = 0f;
        }

        public void UpdateState(BaseEnemy enemy)
        {
            
            LookAtPlayer(enemy);

            attackTimer += Time.deltaTime;

            if (attackTimer >= timeBetweenAttacks)
            {
                AttackPlayer(enemy);
                attackTimer = 0f;  // Reset the timer after attacking
            }
        }

        public void ExitState(BaseEnemy enemy)
        {
            enemy.StopAttackAnimations();
        }

        private void LookAtPlayer(BaseEnemy enemy)
        {
            
            Vector3 lookDirection = enemy.player.position - enemy.transform.position;
            lookDirection.y = 0;  // Keep the enemy's rotation in the horizontal plane
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.rotationSpeed);
        }

        private void AttackPlayer(BaseEnemy enemy)
        {
            enemy.FireProjectile();
        }
    }
}