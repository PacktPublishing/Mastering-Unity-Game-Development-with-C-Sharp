using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace FusionFuryGame
{
    public abstract class BaseProjectile : MonoBehaviour, IDamage 
    {
        private float damage;
        public float lifetime = 5f; // Lifetime of the projectile in seconds
        private float timer;

        private void OnEnable()
        {
            timer = 0f; // Reset the timer when the projectile is enabled
        }

        private void Update()
        {
            timer += Time.deltaTime;

            // Deactivate the projectile if it exceeds its lifetime
            if (timer >= lifetime)
            {
                DeactivateObject();
            }
        }

        public virtual void SetDamageValue(float value)
        {
            Debug.Log("Set Damage Value: " + value);
            damage = value;
        }

        public virtual float GetDamageValue()
        {
            Debug.Log("Get Damage Value: " + damage);
            return damage;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            // Deactivate projectile when it collides with something
            DeactivateObject();
        }

        public virtual void DeactivateObject()
        {
            gameObject.SetActive(false);
        }

    }
}