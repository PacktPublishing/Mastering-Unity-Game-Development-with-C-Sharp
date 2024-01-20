using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public interface IProjectile : IDamage
    {
        void Fire();  // Method to trigger the firing of the projectile
    }
}