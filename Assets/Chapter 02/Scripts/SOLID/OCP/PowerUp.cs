using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2.OCP
{
    public abstract class PowerUp : MonoBehaviour 
    {
        public abstract void Activate(); // Common activation logic
        public abstract void Deactivate(); // Common deactivation logic
    }

}