using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FusionFuryGame.ChallengeManager;

namespace FusionFuryGame
{
    
    //I will add objects here of the level in the object pool manager
    public class LevelManager : Singlton<LevelManager>
    {
        public GenericDictionary<int, ChallengeType> levelChallengeMapping = new GenericDictionary<int, ChallengeType>();
        public int currentLevel;
        [SerializeField] FloatingText floatingText;
        private void Start()
        {
            PoolManager.Instance.AddNewPoolItem(floatingText.gameObject, 10);

           // StartChallengeForCurrentLevel(currentLevel);
        }
        public void StartChallengeForCurrentLevel(int currentLevel)
        {
            if (levelChallengeMapping.TryGetValue(currentLevel, out ChallengeType challengeType))
            {
                // Start the challenge associated with the current level
                ChallengeManager.Instance.StartChallenge(challengeType);
            }
            else
            {
                Debug.LogError($"No challenge mapped for Level {currentLevel}");
            }
        }
       
    }
}