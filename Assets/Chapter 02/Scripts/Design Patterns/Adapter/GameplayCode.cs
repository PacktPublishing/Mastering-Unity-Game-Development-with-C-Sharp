using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdapterPattern
{
    public class GameplayCode : MonoBehaviour
    {
        private void Start()
        {
            LibraryA libraryA = new LibraryA();
            LibraryB libraryB = new LibraryB();
            IAudioPlayer audioPlayer = new AudioAdapter(libraryA, libraryB);

            // Play voice-over using the Adapter (Library B)
            audioPlayer.Play("voice_over");

            // Play sound effect using Library A
            audioPlayer.Play("explosion_sound");
        }

    }
}
