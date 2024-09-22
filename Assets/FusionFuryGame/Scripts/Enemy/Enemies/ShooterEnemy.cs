using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class ShooterEnemy : BaseEnemy
    {
        public override void AttackPlayer()
        {
            shootComponent.FireShot();
        }
    }
}
