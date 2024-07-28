using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FusionFuryGame
{
    public class PlayerShoot : MonoBehaviour
    {
        public static UnityAction onFire = delegate { };
        [SerializeField] BaseWeapon currentWeapon;
        [SerializeField] private float fireDamage;
        [SerializeField] private float shootingInterval = 0.5f;  // Set the shooting interval in seconds
        private float timeSinceLastShot = 0f;
        private void Start()
        {
            ObjectPoolManager.Instance.CreateObjectPool(currentWeapon.weaponData.projectileData.attachedProjectile.gameObject, 50, "PlayerProjectile", currentWeapon.transform);
        }

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



        public void OnShootFire()
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


        public void ShootForAbility()
        {
            // Special shooting logic for abilities
            currentWeapon.Shoot(fireDamage);
        }
    }
}
