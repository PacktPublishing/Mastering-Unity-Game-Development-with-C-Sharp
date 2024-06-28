using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace FusionFuryGame
{
    public abstract class BaseProjectile : MonoBehaviour, IDamage 
    {
        [SerializeField] private ProjectileData projectileData;  // Reference to the ProjectileData ScriptableObject

        private float timer;
        private Vector3 direction;

        private void OnEnable()
        {
            timer = 0f; // Reset the timer when the projectile is enabled
            //transform.position = Vector3.zero;
            if (projectileData != null)
            {
                // Optionally, you could initialize other properties from projectileData here
                SetDamageValue(projectileData.damage);
            }
        }

        public void SetDirection(Vector3 dir)
        {
            direction = dir.normalized;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            // Move the projectile forward
            transform.Translate(Vector3.forward * projectileData.speed * Time.deltaTime);

            // Deactivate the projectile if it exceeds its lifetime
            if (timer >= projectileData.lifetime)
            {
                DeactivateObject();
            }
        }

        public virtual void SetDamageValue(float value)
        {
            Debug.Log("Set Damage Value: " + value);
            projectileData.damage = value;
        }

        public virtual float GetDamageValue()
        {
            Debug.Log("Get Damage Value: " + projectileData.damage);
            return projectileData.damage;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            // Optionally, spawn a hit effect at the hit point
            if (projectileData.hitEffectPrefab != null)
            {
                Instantiate(projectileData.hitEffectPrefab, transform.position, Quaternion.identity);
            }

            // Deactivate projectile when it collides with something
            DeactivateObject();
        }

        public virtual void DeactivateObject()
        {
            gameObject.SetActive(false);
        }

    }
}