using UnityEngine;
using UnityEngine.AI;

namespace FusionFuryGame
{
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        private void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            animator.SetFloat("Speed", navMeshAgent.speed);
        }
        public void StartAttackAnimations()
        {
            animator.SetBool("IsAttacking", true);
        }

        public void StopAttackAnimations()
        {
            animator.SetBool("IsAttacking", false);
        }

    }
}
