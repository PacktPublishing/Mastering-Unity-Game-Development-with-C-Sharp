using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class AttackState : IEnemyState
    {
        public float timeBetweenAttacks = 2f; // Time between attacks in seconds
        private float lastAttackTime = -Mathf.Infinity;

        public void EnterState(BaseEnemy baseEnemy)
        {
            baseEnemy.animationComponent.StartAttackAnimations();  
        }

        public void UpdateState(BaseEnemy enemy)
        {
            
            LookAtPlayer(enemy);

            // Check if enough time has passed since the last attack
            if (Time.time >= lastAttackTime + timeBetweenAttacks)
            {
                // Execute the attack logic
                AttackPlayer(enemy);

                // Update the last attack time to the current time
                lastAttackTime = Time.time;
            }
        }



        public void ExitState(BaseEnemy enemy)
        {
           
            enemy.animationComponent.StopAttackAnimations();
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
            enemy.shootComponent.FireShot();
        }
    }
}