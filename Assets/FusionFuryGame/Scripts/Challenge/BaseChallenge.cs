using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public abstract class BaseChallenge : MonoBehaviour
    {
        public CommonChallengeData commonData;

        public abstract void StartChallenge();
        public abstract void CompleteChallenge();
    }
}
