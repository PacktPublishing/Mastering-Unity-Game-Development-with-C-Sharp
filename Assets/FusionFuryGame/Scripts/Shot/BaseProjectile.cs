using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
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