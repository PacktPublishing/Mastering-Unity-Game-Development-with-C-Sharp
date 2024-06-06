using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerStats playerStats;

        public Transform groundChecker;
        public LayerMask groundLayer;
        public float groundDistance;

        public Rigidbody playerRigidbody;
        private bool isGrounded = true;
        private bool canDash = true;

        private Vector3 movementVector;

        public Camera mainCamera;

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

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void MovePlayer(Vector2 input)
        {
            movementVector = input;
        }

        private void MovePlayer()
        {
            Vector3 movement = new Vector3(movementVector.x , 0f , movementVector.y) * playerStats.MoveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                playerRigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void Dash()
        {
            if (canDash)
            {        
                Vector3 dashVector = new Vector3(movementVector.x, 0f, movementVector.y).normalized;
                playerRigidbody.AddForce(dashVector * playerStats.DashForce, ForceMode.Impulse);

                canDash = false;
                Invoke(nameof(ResetDash), playerStats.DashCooldown);
            }
        }
        private void Update()
        {
            RotatePlayerToMouse();
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


        private void RotatePlayerToMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
            {
                Vector3 targetPosition = hitInfo.point;
                Vector3 direction = (targetPosition - transform.position).normalized;
                direction.y = 0; // Keep the direction strictly horizontal
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation; // Directly set the rotation
            }
        }
    }
}