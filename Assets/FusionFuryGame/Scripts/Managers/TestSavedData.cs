using FusionFuryGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSavedData : Singlton<TestSavedData>
{
    public PlayerData playerData;
    public GameSettings gameSettings;


    public void DisplayData()
    {
        Debug.Log("player Data " + playerData.playerLevel);
    }
}
