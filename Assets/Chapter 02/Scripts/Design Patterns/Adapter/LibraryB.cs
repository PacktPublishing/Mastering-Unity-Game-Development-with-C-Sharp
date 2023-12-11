using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdapterPattern
{
    public class LibraryB
    {
        public void Play(AudioClip audioClip)
        {
            Debug.Log($"Library B playing audio clip: {audioClip.name}");
            // Implementation for playing audio clip...
        }

    }
}