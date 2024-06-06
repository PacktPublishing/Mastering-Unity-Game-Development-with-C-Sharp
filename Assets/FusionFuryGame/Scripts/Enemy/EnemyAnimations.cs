using UnityEngine;

namespace FusionFuryGame
{
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            //animator = GetComponent<Animator>();
        }


        public void StartAttackAnimations()
        {
            //animator.SetBool("IsAttacking", true);
        }

        public void StopAttackAnimations()
        {
            //animator.SetBool("IsAttacking", false);
        }

    }
}
