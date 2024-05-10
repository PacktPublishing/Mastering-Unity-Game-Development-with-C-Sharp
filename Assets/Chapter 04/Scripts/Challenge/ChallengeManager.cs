using UnityEngine;


namespace Chapter4
{
    public class ChallengeManager : Singleton<ChallengeManager>
    {
        public GenericDictionary<ChallengeType, BaseChallenge> challengeDictionary = new GenericDictionary<ChallengeType, BaseChallenge>();
        public void StartChallenge(ChallengeType challengeType)
        {
            if (challengeDictionary.TryGetValue(challengeType, out BaseChallenge challengeScript))
            {
               if (!challengeScript.commonData.isCompleted)
                {
                    SetCurrentChallenge(challengeScript);
                    currentChallenge.StartChallenge();
                }
                else
                {
                    Debug.Log("Challenge already completed!");
                }
            }
            else
            {
                Debug.LogError($"No challenge script found for ChallengeType {challengeType}");
            }
        }

        private BaseChallenge currentChallenge;
        private void SetCurrentChallenge(BaseChallenge challengeScript)
        {
            if (currentChallenge != null)
            {
                currentChallenge.CompleteChallenge();
            }
            currentChallenge = challengeScript;
        }
    }

}