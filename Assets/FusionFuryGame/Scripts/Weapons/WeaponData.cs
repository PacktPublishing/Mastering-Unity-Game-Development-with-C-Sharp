using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FusionFuryGame
{

    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]

    public class WeaponData : ScriptableObject
    {
        public float weaponPower;
        public float projectileForce;
        public float fireRate;
        public float accuracy;
        public float reloadTime;
        public int magazineSize;
        public float recoil;
        public float range;
        public int burstFireCount;
        public FireMode fireMode;
        public float bulletSpread;
        public ProjectileData projectileData;  // Reference to the ProjectileData ScriptableObject

    }

    public enum FireMode
    {
        Single,
        Burst,
        Automatic
    }
}