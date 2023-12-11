using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface IHealthSubject 
    {
        event Action<int> OnHealthChanged;
    }
}