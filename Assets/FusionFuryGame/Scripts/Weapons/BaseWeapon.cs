using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] public WeaponData weaponData;  // Reference to the WeaponData ScriptableObject
        [SerializeField] public Transform muzzleTransfrom;
        public LineRenderer lineRenderer;
        public float lineDuration = 0.1f;
        public GameObject hitEffectPrefab;

        private int currentAmmo;
        private bool isReloading = false;
        private float nextFireTime = 0f;

        private void Start()
        {
            currentAmmo = weaponData.magazineSize;
        }

        public virtual void Shoot(float fireDamage)
        {
            if (isReloading) return;

            if (Time.time < nextFireTime)
            {
                return;  // Return if it's not time to fire yet
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            nextFireTime = Time.time + 1f / weaponData.fireRate;  // Calculate the next fire time based on the fire rate
            currentAmmo--;

            if (weaponData.fireMode == FireMode.Burst)
            {
                StartCoroutine(BurstFire(fireDamage));
            }
            else
            {
                FireProjectile(fireDamage);
            }
        }

        private IEnumerator BurstFire(float fireDamage)
        {
            for (int i = 0; i < weaponData.burstFireCount; i++)
            {
                if (currentAmmo <= 0) break;
                FireProjectile(fireDamage);
                currentAmmo--;
                yield return new WaitForSeconds(1f / weaponData.fireRate);
            }
        }

        private void FireProjectile(float fireDamage)
        {
            // Instantiate the projectile from the object pool
            GameObject projectileObject = ObjectPoolManager.Instance.GetPooledObject(weaponData.projectileData.tag);

            if (projectileObject != null)
            {
                // Set the position of the projectile to the gun's position
                //projectileObject.transform.position = transform.position;
                projectileObject.transform.position = muzzleTransfrom.position;
                projectileObject.transform.rotation = muzzleTransfrom.rotation;

                // Apply bullet spread for accuracy
                Vector3 spread = Vector3.forward +
                                 new Vector3(
                                     Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread),
                                     Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread),
                                     Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread));
                spread.Normalize();

                // Get the component from the instantiated projectile
                BaseProjectile projectileComponent = projectileObject.GetComponent<BaseProjectile>();

                if (projectileComponent != null)
                {
                    // Modify the fire damage by adding the current weapon's power
                    float modifiedDamage = fireDamage + weaponData.weaponPower;

                    // Set the damage value on the instantiated projectile
                    projectileComponent.SetDamageValue(modifiedDamage);

                    // Set the direction for the projectile
                    projectileComponent.SetDirection(muzzleTransfrom.position);
                    // Apply recoil
                   // ApplyRecoil();
                }
                else
                {
                    Debug.LogWarning("Instantiated projectile is missing BaseProjectile component.");
                }
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(weaponData.reloadTime);
            currentAmmo = weaponData.magazineSize;
            isReloading = false;
        }

        private void ApplyRecoil()
        {
            // Implement recoil logic, e.g., move the weapon backward
            // This is just a placeholder for actual recoil implementation
            transform.localPosition -= Vector3.back * weaponData.recoil;
        }

        // Method for the line renderer shooting
        public void ShootWithLineRenderer(float fireDamage)
        {
            if (isReloading) return;

            if (Time.time < nextFireTime)
            {
                return;  // Return if it's not time to fire yet
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            nextFireTime = Time.time + 1f / weaponData.fireRate;  // Calculate the next fire time based on the fire rate
            currentAmmo--;

            Ray ray = new Ray(transform.position, Vector3.forward);
            RaycastHit hit;

            lineRenderer.SetPosition(0, transform.position);

            if (Physics.Raycast(ray, out hit, weaponData.range))
            {
                lineRenderer.SetPosition(1, hit.point);

                IHealth damageable = hit.collider.GetComponent<IHealth>();
                if (damageable != null)
                {
                    damageable.TakeDamage(fireDamage + weaponData.weaponPower);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * weaponData.range);
            }

            StartCoroutine(DisplayLine());
        }

        private IEnumerator DisplayLine()
        {
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(lineDuration);
            lineRenderer.enabled = false;
        }

        // Method for raycast shooting
        public void ShootRaycast(float fireDamage)
        {
            if (isReloading) return;

            if (Time.time < nextFireTime)
            {
                return;  // Return if it's not time to fire yet
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            nextFireTime = Time.time + 1f / weaponData.fireRate;  // Calculate the next fire time based on the fire rate
            currentAmmo--;

            Ray ray = new Ray(transform.position, Vector3.forward);
            RaycastHit hit;
            // Draw the ray in the scene view for debugging purposes
            Debug.DrawRay(ray.origin, ray.direction * weaponData.range, Color.red, 1.0f);
            if (Physics.Raycast(ray, out hit, weaponData.range))
            {
                IHealth damageable = hit.collider.GetComponent<IHealth>();
                if (damageable != null)
                {
                    damageable.TakeDamage(fireDamage + weaponData.weaponPower);
                }

                // Optionally, spawn a hit effect at the hit point
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
