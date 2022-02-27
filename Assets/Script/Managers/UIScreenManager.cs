using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{

    private Text highScore;
    private Text scoreText;
    public GameObject[] UIScreens;
    static UIScreenManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public static UIScreenManager GetInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowScreen()
    {
        switch ((int)GameManager.GetInstance().PlayerStatus)
        {
            case 0:
                UIScreens[0].SetActive(true);
                UIScreens[1].SetActive(false);
                UIScreens[2].SetActive(false);
                UIScreens[3].SetActive(false);
                break;
            case 1:
                UIScreens[0].SetActive(false);
                UIScreens[1].SetActive(true);
                UIScreens[2].SetActive(true);
                UIScreens[3].SetActive(false);
                UpdateScreen();
                break;
            case 2:
                UIScreens[0].SetActive(false);
                UIScreens[1].SetActive(false);
                UIScreens[2].SetActive(true);
                UIScreens[3].SetActive(false);
                break;
            case 3:
                UIScreens[0].SetActive(false);
                UIScreens[1].SetActive(false);
                UIScreens[2].SetActive(false);
                UIScreens[3].SetActive(true);
                UpdateScreen();
                break;
        }    
    }

    public void UpdateScreen()
    {
        foreach (var item in UIScreens)
        {
            if (item.activeSelf)
            {
                if(item.name == "HighScoreText")
                highScore = item.transform.GetComponentInChildren<Text>();

                if (item.name == "ScoreText")
                scoreText = item.transform.GetComponentInChildren<Text>();
            }
        }

        if(highScore != null)
        {
            UpdateHighScore();
            //highScore.text = "HIGHSCORE " + Score.GetHighScore().ToString();
        }

        if(scoreText != null)
        {
            scoreText.text = Character_Controller.GetInstance().FishCount.ToString();
        }
    }


    void UpdateHighScore()
    {
        if (Character_Controller.GetInstance().FishCount >= Score.GetHighScore())
        {
            highScore.text = "NEW HIGHSCORE";

        }
        else
        {
            highScore.text = "HIGHSCORE " + Score.GetHighScore();
        }
    }

}
