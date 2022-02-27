using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private Text highScore;
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        highScore = transform.Find("HighScoreText").GetComponent<Text>();
    }

    private void Start()
    {
        highScore.text = "HIGHSCORE " + Score.GetHighScore().ToString();
    }

    private void Update()
    {
        scoreText.text = Character_Controller.GetInstance().FishCount.ToString();

    }
}
