using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static void Start()
    {
        Character_Controller.GetInstance().OnDied += Bird_OnDied;
    }

    private static void Bird_OnDied(object sender, EventArgs e)
    {
        //TrySetNewHighScore(Character_Controller.GetInstance().FishCount);
        ResetHighScore();
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("highscore");
    }

    public static bool TrySetNewHighScore(int score)
    {
        int currentHighScore = GetHighScore();
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            Console.WriteLine("SET NEW HIGH SCORE");
            return true;
        }
        else
        {
            return false;
        }

    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }
}
