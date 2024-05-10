using UnityEngine;


namespace Chapter4
{
    public abstract class BaseChallenge : MonoBehaviour
    {
        public CommonChallengeData commonData;
        public abstract void StartChallenge();
        public abstract void CompleteChallenge();
    }

}