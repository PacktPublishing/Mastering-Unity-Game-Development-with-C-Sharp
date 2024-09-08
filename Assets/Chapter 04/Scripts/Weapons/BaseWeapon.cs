using FusionFuryGame;
using UnityEngine;

namespace Chapter4
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected BaseProjectile attachedProjectile;
        [SerializeField] protected float weaponPower;
        [SerializeField] protected Transform muzzleTransform;
        [SerializeField] protected float projectileForce;

        public virtual void Shoot(float fireDamage)
        {
            // Instantiate the projectile from the object pool 
            GameObject projectileObject = PoolManager.Instance.GetPooledObject(attachedProjectile.name);

            if (projectileObject != null)
            {
                // Set the position of the projectile to the gun's muzzle position 
                projectileObject.transform.position = muzzleTransform.position;

                // Get the rigid body component from the projectile 
                Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();

                if (projectileRb != null)
                {
                    // Apply force to the projectile in the forward vector of the weapon 
                    projectileRb.AddForce(muzzleTransform.forward * projectileForce, ForceMode.Impulse);

                    // Modify the fire damage by adding the current weapon's power 
                    float modifiedDamage = fireDamage + weaponPower;

                    // Apply damage and other logic to the projectile (consider implementing IDamage interface) 
                    attachedProjectile.SetDamageValue(modifiedDamage);

                }
                else
                {
                    // Handle if the projectile doesn't have a rigid body 
                    Debug.LogWarning("Projectile prefab is missing Rigidbody component.");

                }

            }

        }

    }
}