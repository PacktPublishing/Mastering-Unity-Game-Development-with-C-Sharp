using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] public BaseProjectile attachedProjectile;
        [SerializeField] protected float weaponPower;
        [SerializeField] protected Transform muzzleTransform;
        [SerializeField] protected float projectileForce;


        public LineRenderer lineRenderer;
        public float lineDuration = 0.1f;

        public GameObject hitEffectPrefab;

        public virtual void Shoot(float fireDamage)
        {
            // Instantiate the projectile from the object pool
            GameObject projectileObject = ObjectPoolManager.Instance.GetPooledObject(attachedProjectile.tag);

            if (projectileObject != null)
            {
                // Set the position of the projectile to the gun's muzzle position
                projectileObject.transform.position = muzzleTransform.position;
                projectileObject.transform.rotation = muzzleTransform.rotation;

                // Get the component from the instantiated projectile
                BaseProjectile projectileComponent = projectileObject.GetComponent<BaseProjectile>();

                if (projectileComponent != null)
                {
                    // Modify the fire damage by adding the current weapon's power
                    float modifiedDamage = fireDamage + weaponPower;

                    // Set the damage value on the instantiated projectile
                    projectileComponent.SetDamageValue(modifiedDamage);

                    // Get the rigid body component from the projectile
                    Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();

                    if (projectileRb != null)
                    {
                        // Apply force to the projectile in the player's forward direction
                        projectileRb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
                    }
                    else
                    {
                        // Handle if the projectile doesn't have a rigid body
                        Debug.LogWarning("Projectile prefab is missing Rigidbody component.");
                    }
                }
                else
                {
                    Debug.LogWarning("Instantiated projectile is missing BaseProjectile component.");
                }
            }
        }


        public void ShootWithLineRenderer(float fireDamage)
        {
            Ray ray = new Ray(muzzleTransform.position, transform.forward);
            RaycastHit hit;

            lineRenderer.SetPosition(0, muzzleTransform.position);

            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(1, hit.point);

                IHealth damageable = hit.collider.GetComponent<IHealth>();
                if (damageable != null)
                {
                    damageable.TakeDamage(fireDamage + weaponPower);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 100f);
            }

            StartCoroutine(DisplayLine());
        }

        private IEnumerator DisplayLine()
        {
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(lineDuration);
            lineRenderer.enabled = false;
        }



        public void ShootRaycast(float fireDamage)
        {
            Ray ray = new Ray(muzzleTransform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                IHealth damageable = hit.collider.GetComponent<IHealth>();
                if (damageable != null)
                {
                    damageable.TakeDamage(fireDamage + weaponPower);
                }

                // Optionally, spawn a hit effect at the hit point
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
