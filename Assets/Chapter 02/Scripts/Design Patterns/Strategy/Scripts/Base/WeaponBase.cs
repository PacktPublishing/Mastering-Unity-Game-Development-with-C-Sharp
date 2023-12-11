using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public abstract void Shoot();
    }
}