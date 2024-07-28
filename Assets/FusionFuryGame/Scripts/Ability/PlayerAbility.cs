using UnityEngine;
using UnityEngine.Events;

namespace FusionFuryGame
{



    public class PlayerAbility : MonoBehaviour
    {
        public static UnityAction OnAbilityActivated = delegate { };
        public static UnityAction OnAbilityDeactivated = delegate { };
        private IAbility currentAbility;
        private PlayerShoot playerShoot;
        private PlayerMovement playerMovement;

        private void OnEnable()
        {
            PlayerInput.onAbility += OnAbilityPressed;
        }

        private void OnDisable()
        {
            PlayerInput.onAbility -= OnAbilityPressed;
        }

        private void Start()
        {
            playerShoot = GetComponent<PlayerShoot>();
            playerMovement = GetComponent<PlayerMovement>();

            IAbility rotateAndShoot = new RotateAndShootAbility(360f, 2, 0.5f, 1f);

            // Assign the ability to the player
            SetAbility(rotateAndShoot);
        }

        public void SetAbility(IAbility ability)
        {
            currentAbility = ability;
        }

        public void UseAbility(PlayerShoot playerShoot, PlayerMovement playerMovement)
        {
            if (currentAbility != null)
            {
                OnAbilityActivated.Invoke();
                currentAbility?.Activate(playerShoot, playerMovement);
            }
        }

        private void OnAbilityPressed()
        {
            UseAbility(playerShoot, playerMovement);
        }

        public void AbilityFinished()
        {
            Debug.Log("On Ability Finished ");
            OnAbilityDeactivated.Invoke();
        }

    }

}