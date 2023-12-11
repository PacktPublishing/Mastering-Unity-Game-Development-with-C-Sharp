using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public interface IEntity 
    {
        Transform transform { get; }
    }
}