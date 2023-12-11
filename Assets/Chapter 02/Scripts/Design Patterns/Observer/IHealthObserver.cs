using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface IHealthObserver 
    {
        void OnHealthChanged(int health);
    }
}