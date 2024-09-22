using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class MovableEnemy : BaseEnemy
    {
        [SerializeField] private float rollingSpeed = 5f; // Speed at which the enemy rolls towards the player

        protected override void OnEnable()
        {
            base.OnEnable();
            transform.DOScale(Vector3.one * 15f, 1f).SetEase(Ease.OutBounce); // You can tweak duration and easing as needed
        }


        public override void AttackPlayer()
        {
            //here i need to start the roll animation and move to the player
            animationComponent.StartAttackAnimations();
            Debug.Log("Attack Player ");
            // Move towards the player
            StartCoroutine(MoveTowardsPlayer());
        }

        private IEnumerator MoveTowardsPlayer()
        {
            while (Vector3.Distance(transform.position, player.position) > attackRange)
            {
                // Move towards the player using NavMeshAgent
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.speed = rollingSpeed;

                yield return null; // Wait for the next frame
            }

            // Stop the rolling animation once the enemy is within range
            animationComponent.StopAttackAnimations();

            // Stop the movement
            navMeshAgent.ResetPath();

            // Trigger the hit/attack on the player here
            PerformAttack();
        }

        private void PerformAttack()
        {
            // Logic to handle when the enemy hits the player
            // For example, dealing damage to the player
            Debug.Log("Enemy has hit the player!");
        }
    }
}