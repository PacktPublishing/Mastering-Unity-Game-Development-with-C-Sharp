using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class Bullet : BaseProjectile
    {
        public override void DeactivateObject()
        {
            ObjectPoolManager.Instance.ReturnToPool("PlayerProjectile", gameObject);
        }
    }
}
