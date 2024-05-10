using UnityEngine.Events;
using UnityEngine;


namespace Chapter4
{
    public class PlayerShoot : MonoBehaviour
    {
        public static UnityAction onFire = delegate { };
        [SerializeField] BaseWeapon currentWeapon;
        [SerializeField] private float fireDamage;
        [SerializeField] private float shootingInterval = 0.5f;  // Set the shooting interval in seconds 
        private float timeSinceLastShot = 0f;

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        private void OnEnable()
        {
            PlayerInput.onShoot += OnShootFire;
        }


        private void OnDisable()
        {
            PlayerInput.onShoot -= OnShootFire;
        }

        private void OnShootFire()
        {
            // Check if enough time has passed since the last shot 
            if (timeSinceLastShot >= shootingInterval)
            {
                // Shoot in the forward vector of the weapon and pass player power stat 
                currentWeapon.Shoot(fireDamage);

                // Reset the timer 
                timeSinceLastShot = 0f;

                // Invoke the onFire event 
                onFire.Invoke();
            }

        }

    }

}