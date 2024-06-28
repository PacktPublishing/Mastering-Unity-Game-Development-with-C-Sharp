using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    [CreateAssetMenu(fileName = "NewProjectileData", menuName = "Projectile/ProjectileData")]

    public class ProjectileData : ScriptableObject
    {
        public float damage;
        public float lifetime = 5f;
        public GameObject hitEffectPrefab;
        public float speed;
        public GameObject attachedProjectile;
        public string tag;
    }
}

