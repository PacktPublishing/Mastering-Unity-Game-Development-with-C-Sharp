using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryPattern
{
    public class Rocket : Projectile
    {
        public override string Name => "Rocket";
        public override void Create()
        {
            Debug.Log("I'm creating Rocket ");
        }
    }
}