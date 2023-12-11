using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AdapterPattern
{
    public interface IAudioPlayer 
    {
        void Play(string soundName);
    }
}