using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdapterPattern
{
    public class LibraryA 
    {
        public void PlaySoundEffect(string soundName)
        {
            Debug.Log($"Library A playing sound effect: {soundName}");
            // Implementation for playing sound effect...
        }

        public void PlayMusic(string musicName)
        {
            Debug.Log($"Library A playing music: {musicName}");
            // Implementation for playing music...
        }

    }
}