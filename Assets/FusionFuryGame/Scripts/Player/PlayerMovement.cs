using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
        private bool canRotate = true;
        private bool canMove = true;

        private Vector2 movementInput;
        private Vector3 movementVector;

        public Camera mainCamera;
        public Transform playerGraphics; // The graphics object of the player
        private bool isDashing = false;
        private float dashTime = 0.2f; // Time the dash force is active
        private Vector3 dashVelocity;
        private float decelerationDuration = 0.5f; // Time it takes to decelerate to stop


        public static UnityAction<float> onMove = delegate { };
        public static UnityAction<float> OnDashStarted = delegate { };
        public static UnityAction OnDashEnded = delegate { };

        private void OnEnable()
        {
            PlayerInput.onDash += Dash;
            PlayerInput.onMovement += HandleMovementInput;
            PlayerAbility.OnAbilityActivated += DisableRotation;
            PlayerAbility.OnAbilityDeactivated += EnableRotation;
            PlayerHealth.onPlayerDied += StopMovement;
        }

        private void OnDisable()
        {
            PlayerInput.onDash -= Dash;
            PlayerInput.onMovement -= HandleMovementInput;
            PlayerAbility.OnAbilityActivated -= DisableRotation;
            PlayerAbility.OnAbilityDeactivated -= EnableRotation;
            PlayerHealth.onPlayerDied -= StopMovement;
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

            // Send movement magnitude to animation
            onMove.Invoke(movementVector.magnitude);
        }

        private void Dash()
        {
            if (canDash && movementInput != Vector2.zero) // Only dash if player is moving
            {
                Vector3 dashDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
                dashVelocity = dashDirection * playerStats.DashForce;

                // Apply dash
                playerRigidbody.MovePosition(playerRigidbody.position + dashVelocity * Time.fixedDeltaTime);

                // Start cooldown and deceleration process
                canDash = false;
                isDashing = true;
                Invoke(nameof(ResetDash), playerStats.DashCooldown);

                // Trigger dash start event
                OnDashStarted.Invoke(dashTime);

                // Begin smooth deceleration after dash time
                StartCoroutine(SmoothDecelerate());
            }
        }

        //private IEnumerator SmoothPaniniProjection(float targetValue, float duration)
        //{
        //    float elapsedTime = 0f;
        //    float startValue = paniniProjection.distance.value;

        //    // Smoothly transition to the target value
        //    while (elapsedTime < duration)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        float newPaniniDistance = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

        //        // Update the Panini projection distance in the post-processing volume
        //        paniniProjection.distance.value = newPaniniDistance;

        //        yield return null;
        //    }

        //    // Ensure we reach the exact target value
        //    paniniProjection.distance.value = targetValue;

        //    // Wait for dash to finish and revert the effect
        //    yield return new WaitForSeconds(dashTime);

        //    // Smoothly transition back to the original value
        //    elapsedTime = 0f;
        //    while (elapsedTime < duration)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        float newPaniniDistance = Mathf.Lerp(targetValue, originalPaniniDistance, elapsedTime / duration);

        //        paniniProjection.distance.value = newPaniniDistance;

        //        yield return null;
        //    }

        //    // Ensure the original value is restored
        //    paniniProjection.distance.value = originalPaniniDistance;
        //}

        private IEnumerator SmoothDecelerate()
        {
            float elapsedTime = 0f;
            Vector3 currentVelocity = dashVelocity;
            Vector3 targetVelocity = Vector3.zero; // Stop the player eventually

            // Wait for the dash duration to end before deceleration starts
            yield return new WaitForSeconds(dashTime);

            // Smoothly decelerate player
            while (elapsedTime < decelerationDuration)
            {
                elapsedTime += Time.deltaTime;

                // Linearly interpolate velocity from current dash speed to zero
                Vector3 newVelocity = Vector3.Lerp(currentVelocity, targetVelocity, elapsedTime / decelerationDuration);

                // Apply new velocity as movement
                playerRigidbody.MovePosition(playerRigidbody.position + newVelocity * Time.fixedDeltaTime);

                yield return null;
            }

            // Ensure the player stops completely
            playerRigidbody.velocity = Vector3.zero;
            isDashing = false;
            // Trigger dash end event
            OnDashEnded.Invoke();
        }


        private void Update()
        {
            if (canRotate)
            {
                RotatePlayerToMouse();
            }
        }

        private void FixedUpdate()
        {
            if (canMove)
                MovePlayer();

        }

        private void ResetDash()
        {
            canDash = true;
        }

        private void RotatePlayerToMouse()
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            if (playerPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 lookAtPoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                transform.LookAt(lookAtPoint);

                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);

            }
        }

        public Vector3 GetMouseAimDirection()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            if (playerPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 aimDirection = (hitPoint - transform.position).normalized;
                aimDirection.y = 0f;  // Ignore vertical aiming
                return aimDirection;
            }

            return transform.forward;  // Fallback in case of an error
        }

        private void DisableRotation()
        {
            canRotate = false;
        }

        private void EnableRotation()
        {
            canRotate = true;
        }

        private void StopMovement()
        {
            canRotate = false;
            canMove = false;
        }
    }
}