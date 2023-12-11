using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.OCP
{
    public class PowerUpManager : MonoBehaviour
    {
        private void Start()
        {
            // Example of using the power-up system
            PowerUp doubleJump = new DoubleJumpPowerUp();
            AddPowerUp(doubleJump);

            PowerUp invincibility = new TemporaryInvincibilityPowerUp();
            AddPowerUp(invincibility);
        }

        private void AddPowerUp(PowerUp powerUp)
        {
            powerUp.Activate();
            // Logic for adding power-up to the game
        }

        private void RemovePowerUp(PowerUp powerUp)
        {
            powerUp.Deactivate();
            // Logic for removing power-up from the game
        }

    }
}