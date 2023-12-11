using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.SRP
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateAnimation(bool isRunning)
        {
            animator.SetBool("IsRunning", isRunning);
        }

    }
}
