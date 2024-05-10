using UnityEngine;

namespace Chapter4
{
    public abstract class BaseProjectile : MonoBehaviour, IDamage
    {
        private float damage;

        public virtual void SetDamageValue(float value)
        {
            damage = value;
        }

        public float GetDamageValue()
        {
            return damage;
        }

    }
}