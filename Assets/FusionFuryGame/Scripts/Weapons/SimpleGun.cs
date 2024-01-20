using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class SimpleGun : BaseWeapon
    {
        public override void Shoot( float fireDamage)
        {
            base.Shoot( fireDamage );
            //Add here special logic for the gun if needed
        }
    }
}