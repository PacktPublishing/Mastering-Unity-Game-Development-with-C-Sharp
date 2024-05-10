using UnityEngine;


namespace Chapter4
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