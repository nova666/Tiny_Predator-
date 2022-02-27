using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{

    private Text scoreText;
    private Text highScoreText;
    private GameObject GameOverMenu;

    private void Awake()
    {
        GameOverMenu = transform.GetChild(0).gameObject;
        scoreText = GameOverMenu.transform.Find("ScoreText").GetComponent<Text>();
        highScoreText = GameOverMenu.transform.Find("HighScoreText").GetComponent<Text>();
        Hide();
    }

    private void Start()
    {
        //Bird.GetInstance().OnDied += Bird_OnDied;
        Character_Controller.GetInstance().OnDied += Bird_OnDied;
    }

    private void Bird_OnDied(object sender, EventArgs e)
    {

        scoreText.text = Character_Controller.GetInstance().FishCount.ToString();

        if(Character_Controller.GetInstance().FishCount >= Score.GetHighScore())
        {
            highScoreText.text = "NEW HIGHSCORE";

        }
        else
        {
            highScoreText.text = "HIGHSCORE " + Score.GetHighScore();
        }
        Show();
    }

    private void Hide()
    {
        GameOverMenu.SetActive(false);
    }

    private void Show()
    {
        GameOverMenu.SetActive(true);
    }
}
