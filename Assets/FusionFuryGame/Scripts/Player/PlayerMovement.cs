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
        public float groundDistance = 0.2f;

        public Rigidbody playerRigidbody;
        private bool isGrounded = true;
        private bool canDash = true;

        private Vector2 movementInput;
        private Vector3 movementVector;

        public Camera mainCamera;
        public Transform playerGraphics; // The graphics object of the player

        private void OnEnable()
        {            
            PlayerInput.onDash += Dash;
            PlayerInput.onMovement += HandleMovementInput;
        }

        private void OnDisable()
        {
            PlayerInput.onDash -= Dash;
            PlayerInput.onMovement -= HandleMovementInput;
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void HandleMovementInput(Vector2 input)
        {
            movementInput = input;
        }

        private void MovePlayer()
        {
            movementVector = new Vector3(movementInput.x, 0f, movementInput.y).normalized * playerStats.MoveSpeed;
            Vector3 newPosition = playerRigidbody.position + movementVector * Time.fixedDeltaTime;
            playerRigidbody.MovePosition(newPosition);
        }


        private void Dash()
        {
            if (canDash)
            {
                Vector3 dashVector = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
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
                
                //playerRigidbody.MoveRotation(lookRotation); // Directly set the rotation
                transform.LookAt(new Vector3(hitInfo.point.x, 0, hitInfo.point.z));
            }
        }
    }
}