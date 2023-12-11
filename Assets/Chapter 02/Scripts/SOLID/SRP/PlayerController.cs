using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter2.SRP
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerAnimation playerAnimation;
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;

        private void Start()
        {
            playerAnimation = GetComponent<PlayerAnimation>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            playerMovement.Move();

            if (playerAnimation != null)
            {
                playerAnimation.UpdateAnimation(playerInput.IsRunning());
            }

            if (playerInput.IsJumping())
            {
                playerMovement.Jump();
            }
        }
    }
}