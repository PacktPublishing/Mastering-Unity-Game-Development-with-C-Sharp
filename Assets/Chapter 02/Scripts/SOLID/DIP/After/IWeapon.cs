using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.DIPAfter
{
    // Abstraction for weapons
    public interface IWeapon 
    {
        void Fire();
    }
}