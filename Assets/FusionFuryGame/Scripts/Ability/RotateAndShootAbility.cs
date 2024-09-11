using System.Collections;
using UnityEngine;

namespace FusionFuryGame
{
    public partial class RotateAndShootAbility : IAbility
    {
        private float rotationSpeed;
        private int rotationCycles;
        private float shootingInterval;
        private float slowdownDuration;

        public RotateAndShootAbility(float rotationSpeed, int rotationCycles, float shootingInterval, float slowdownDuration)
        {
            this.rotationSpeed = rotationSpeed;
            this.rotationCycles = rotationCycles;
            this.shootingInterval = shootingInterval;
            this.slowdownDuration = slowdownDuration;
        }

        public void Activate(PlayerShoot playerShoot, PlayerMovement playerMovement)
        {
            RoutineRunner.Instance.StartCoroutine(RotateAndShoot(playerShoot, playerMovement));
        }

        private IEnumerator RotateAndShoot(PlayerShoot playerShoot, PlayerMovement playerMovement)
        {
            float totalRotation = 0;
            float currentSpeed = rotationSpeed;

            while (totalRotation < 360 * rotationCycles)
            {
                float rotationStep = currentSpeed * Time.deltaTime;
                playerMovement.transform.Rotate(Vector3.up, rotationStep);
                totalRotation += rotationStep;

                // Shooting logic here
                if (totalRotation % shootingInterval < rotationStep)
                {
                    playerShoot.ShootForAbility();
                }

                yield return null;
            }
            Debug.Log("Rotate the Player 1");

            float slowdownStep = currentSpeed / slowdownDuration;
            while (currentSpeed > 0)
            {
                currentSpeed -= slowdownStep * Time.deltaTime;
                playerMovement.transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime);
                Debug.Log("Rotate the Player " );

                yield return null;
            }
            Debug.Log("Fishih" );

            playerShoot.GetComponent<PlayerAbility>().AbilityFinished();
        }
    }
}