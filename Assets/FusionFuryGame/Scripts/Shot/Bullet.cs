using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class Bullet : BaseProjectile
    {
        public override void DeactivateObject()
        {
            PoolManager.Instance.ReturnToPool(gameObject ,gameObject.name.Replace("(Clone)", "").Trim());
        }
    }
}
