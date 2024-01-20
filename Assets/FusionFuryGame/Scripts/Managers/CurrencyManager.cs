using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class CurrencyManager : Singlton<CurrencyManager>
    {
        private int currentCoins;
        public void AddCoins(int amount)
        {
            currentCoins += amount;
            Debug.Log($"Coins: {currentCoins}");
        }
    }
}