using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.DIPBefore
{
    public class WeaponManager : MonoBehaviour
    {
        private Pistol pistol;
        private Rifle rifle;

        public WeaponManager()
        {
            pistol = new Pistol();
            rifle = new Rifle();
        }

        public void UseWeapons()
        {
            pistol.Fire();
            rifle.Fire();
        }

    }
}