using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingExample : MonoBehaviour
{
    int highScore = 50;
    
    void Start()
    {
        CheckHighScore(100);
    }

    void CheckHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
        }
    }
}
