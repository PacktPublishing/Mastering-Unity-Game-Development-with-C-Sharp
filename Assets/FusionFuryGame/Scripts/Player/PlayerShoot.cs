using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace FusionFuryGame {
    public class PlayerShoot : MonoBehaviour
    {
        public static UnityAction onFire = delegate { };
        [SerializeField] BaseWeapon currentWeapon;
        [SerializeField] private float fireDamage;
        [SerializeField] private float shootingInterval = 0.5f;  // Set the shooting interval in seconds
        private float timeSinceLastShot = 0f;


        private int currentAmmo;
        [SerializeField] private int maxAmmo = 10;
        [SerializeField] private float reloadTime = 2f;
        private bool isReloading = false;
        private void Start()
        {
            currentAmmo = maxAmmo;
            ObjectPoolManager.Instance.CreateObjectPool(currentWeapon.attachedProjectile.gameObject, 10, "PlayerProjectile" , currentWeapon.transform);
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;

            if (isReloading)
                return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

        }


        private void OnEnable()
        {
            PlayerInput.onShoot += OnShootFire;
            PlayerInput.onReload += OnReload;
        }

        private void OnDisable()
        {
            PlayerInput.onShoot -= OnShootFire;
            PlayerInput.onReload -= OnReload;
        }


        private void OnReload()
        {
            StartCoroutine(Reload());
        }

        private void OnShootFire()
        {
            if (isReloading || currentAmmo <= 0)
                return;

            // Check if enough time has passed since the last shot
            if (timeSinceLastShot >= shootingInterval)
            {
                // Shoot in the forward vector of the weapon and pass player power stat
                currentWeapon.ShootRaycast( fireDamage);

                // Reset the timer
                timeSinceLastShot = 0f;

                // Invoke the onFire event
                onFire.Invoke();

                // Decrease ammo
                currentAmmo--;
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }
}