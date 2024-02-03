using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Data/Game Settings")]

    public class GameSettings : ScriptableObject
    {
        public int soundVolume;
        public bool isFullScreen;
        public int graphicsQuality;       
    }
}