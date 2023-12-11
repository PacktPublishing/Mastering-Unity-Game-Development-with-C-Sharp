using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryPattern
{
    public class Bullet : Projectile
    {
        public override string Name => "Bullet";

         
        public override void Create()
        {
            
            Debug.Log("I'm creating Bullets ");
        }
    }
}