using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public class Blaster : WeaponBase
    {
        public override void Shoot()
        {
            Debug.Log("I'm Blaster ");
        }


    }
}