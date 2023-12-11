using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.DIPAfter
{
    public class WeaponManager 
    {
        private readonly List<IWeapon> weapons;

        public WeaponManager(List<IWeapon> weapons)
        {
            this.weapons = weapons;
        }

        public void UseWeapons()
        {
            foreach (var weapon in weapons)
            {
                weapon.Fire();
            }
        }

    }
}