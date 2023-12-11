using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdapterPattern
{
    public class AudioAdapter : IAudioPlayer
    {
        private readonly LibraryA libraryA;
        private readonly LibraryB libraryB;
        private readonly Dictionary<string, AudioClip> soundToClipMap;

        public AudioAdapter(LibraryA libraryA, LibraryB libraryB)
        {
            this.libraryA = libraryA;
            this.libraryB = libraryB;
            soundToClipMap = new Dictionary<string, AudioClip>();

            // Load or map audio clips here based on sound names
            // For simplicity, let's assume there's a method LoadClip() for loading audio clips.
            soundToClipMap["voice_over"] = LoadClip("voice_over_clip");
        }

        public void Play(string soundName)
        {
            if (soundToClipMap.TryGetValue(soundName, out AudioClip audioClip))
            {
                // Adapter translates the call to Library B's API
                libraryB.Play(audioClip);
            }
            else
            {
                // If soundName is not found in the map, use Library A
                libraryA.PlaySoundEffect(soundName);
            }
        }

        private AudioClip LoadClip(string clipName)
        {
            // Simulated method to load or map audio clips
            return Resources.Load<AudioClip>(clipName);
        }

    }
}