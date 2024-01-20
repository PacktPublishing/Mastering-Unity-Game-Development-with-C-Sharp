using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class ScoreManager : Singlton<ScoreManager>
    {
        private float currentScore;
        private int scoreMultiplier = 1;

        public void ApplyMultiplier(int multiplier)
        {
            scoreMultiplier *= multiplier;
        }
        private void ResetMultiplier()
        {
            scoreMultiplier = 1;
        }
        public void AddScore(int scoreValue)
        {
            // Adjust score based on the current multiplier
            currentScore += scoreValue * scoreMultiplier;

            Debug.Log($"Score: {currentScore}");
        } 
    }
}
