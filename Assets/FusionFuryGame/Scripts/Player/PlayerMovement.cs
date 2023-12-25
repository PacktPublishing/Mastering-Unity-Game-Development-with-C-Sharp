using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public float dashForce = 10f;
        public float dashCooldown = 2f;

        public Transform groundChecker;
        public LayerMask groundLayer;
        public float groundDistance;

        public Rigidbody playerRigidbody;
        private bool isGrounded = true;
        private bool canDash = true;

        private Vector3 movementVector;

        private void OnEnable()
        {
            PlayerInput.onJump += Jump;
            PlayerInput.onDash += Dash;
            PlayerInput.onMovement += MovePlayer;
        }

        private void OnDisable()
        {
            PlayerInput.onJump -= Jump;
            PlayerInput.onDash -= Dash;
            PlayerInput.onMovement -= MovePlayer;
        }

        private void MovePlayer(Vector2 input)
        {
            movementVector = input;
        }

        private void MovePlayer()
        {
            Vector3 movement = new Vector3(movementVector.x , 0f , movementVector.y) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void Dash()
        {
            if (canDash)
            {        
                Vector3 dashVector = new Vector3(movementVector.x, 0f, movementVector.y).normalized;
                playerRigidbody.AddForce(dashVector * dashForce, ForceMode.Impulse);

                canDash = false;
                Invoke(nameof(ResetDash), dashCooldown);
            }
        }


        private void FixedUpdate()
        {
            MovePlayer();
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            isGrounded = Physics.Raycast(groundChecker.position, Vector3.down, groundDistance, groundLayer);
        }

        private void ResetDash()
        {
            canDash = true;
        }
    }
}