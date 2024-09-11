using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

namespace FusionFuryGame
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] public WeaponData weaponData;  // Reference to the WeaponData ScriptableObject
        [SerializeField] public Transform muzzleTransform;
        public LineRenderer lineRenderer;
        public float lineDuration = 0.1f;
        public GameObject hitEffectPrefab;

        private int currentAmmo;
        private bool isReloading = false;
        private float nextFireTime = 0f;

        // Define a fixed firing direction in world space (e.g., Vector3.up for top-down)
        [SerializeField] private Vector3 fixedFireDirection = Vector3.forward;

        private void Start()
        {
            currentAmmo = weaponData.magazineSize;
        }

        public virtual void Shoot(float fireDamage, Vector3 fireDirection)
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


            FireProjectile(fireDamage, fireDirection);

        }

        public void ShootAbility(float fireDamage, Vector3 fireDirection)
        {
            FireProjectile(fireDamage, fireDirection);
        }

        //private IEnumerator BurstFire(float fireDamage)
        //{
        //    for (int i = 0; i < weaponData.burstFireCount; i++)
        //    {
        //        if (currentAmmo <= 0) break;
        //        FireProjectile(fireDamage);
        //        currentAmmo--;
        //        yield return new WaitForSeconds(1f / weaponData.fireRate);
        //    }
        //}

        private void FireProjectile(float fireDamage, Vector3 fireDirection)
        {
            // Get the aim direction based on the player's current aim (using mouse or right stick for example)
           // Vector3 aimDirection = GetAimDirection();

            // Instantiate the projectile from the object pool
            GameObject projectileObject = PoolManager.Instance.GetPooledObject(weaponData.projectileData.attachedProjectile.name);

            if (projectileObject != null)
            {
                // Set the position of the projectile to the muzzle's position
                projectileObject.transform.position = muzzleTransform.position;
                // Set the projectile's rotation to match the aim direction
                projectileObject.transform.rotation = Quaternion.LookRotation(fireDirection);

                // Get the component from the instantiated projectile
                BaseProjectile projectileComponent = projectileObject.GetComponent<BaseProjectile>();

                if (projectileComponent != null)
                {
                    // Modify the fire damage by adding the current weapon's power
                    float modifiedDamage = fireDamage + weaponData.weaponPower;

                    // Set the damage value on the instantiated projectile
                    projectileComponent.SetDamageValue(modifiedDamage);

                    // Set the direction for the projectile (in the player's aim direction)
                    projectileComponent.SetDirection(fireDirection);
                }
                else
                {
                    Debug.LogWarning("Instantiated projectile is missing BaseProjectile component.");
                }
            }
        }

        //// Method to get the aim direction (for mouse aiming or controller aiming)
        //private Vector3 GetAimDirection()
        //{
        //    // Get the mouse position in the world (for mouse-based aiming)
        //    Vector3 mouseWorldPosition = GetMouseWorldPosition();

        //    // Calculate direction from the weapon's position to the mouse position
        //    Vector3 aimDirection = (mouseWorldPosition - muzzleTransform.position).normalized;

        //    // Set the Y position of the aim direction to 0 to ignore vertical aiming
        //    aimDirection.y = 0f;

        //    return aimDirection;
        //}

        //// This method converts mouse screen position into a world position
        //private Vector3 GetMouseWorldPosition()
        //{
        //    // Get the mouse position in screen space
        //    Vector3 mouseScreenPosition = Input.mousePosition;

        //    // Convert screen space position to world position
        //    Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        //    Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assuming the ground plane is flat at y = 0

        //    if (groundPlane.Raycast(ray, out float rayDistance))
        //    {
        //        Vector3 mouseWorldPosition = ray.GetPoint(rayDistance);

        //        // Fix the Y position to a constant value, for example, 1.0f (adjust as needed)
        //        mouseWorldPosition.y = muzzleTransform.position.y;  // Set the Y position of the aim to the height of the muzzle

        //        return mouseWorldPosition;
        //    }

        //    return Vector3.zero; // Fallback value if no hit
        //}

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

            Ray ray = new Ray(transform.position, fixedFireDirection);
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

            Ray ray = new Ray(transform.position, fixedFireDirection);
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
