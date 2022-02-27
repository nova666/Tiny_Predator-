using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {    // Change name to something more appropriate


    #region SCENES LOADING MANAGEMENT
    static GameManager instance;
    State PlayerState;

    // Use this for initialization
    public string[] levelScenes;
    static string[] levels;
    static string SceneName;
    static int LevelIndex = 2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levels = levelScenes;
        instance = this;
        PlayerState = State.Waiting;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public static void LoadNextLevel()
    {
       LevelIndex++;
       SceneManager.LoadScene(levels[LevelIndex]);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(levels[0]);
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene(levels[1]);
    }

    public static void HighScore()
    {
        SceneManager.LoadScene(levels[1]);
    }

    public void Restart()
    {
        instance.StartCoroutine(RestartLevel());
    }


    private static void ReloadLevel()
    {
        SoundManager.GetInstance().PlaySound(SoundManager.Sound.ButtonClick);
        SceneManager.LoadScene(levels[LevelIndex]);
    }

    static IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        ReloadLevel();

    }

    public static void Continue()
    {
        SceneManager.LoadScene(levels[LevelIndex]);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(levels[2]);
    }

    public static void Quit()
    {
        Application.Quit();
    }

    static public string GetSceneName()
    {
        SceneName = SceneManager.GetActiveScene().name;
        return SceneName;
    }


    #region manage Music on Loading new Levels

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        #region
        // Debug.Log(" Level Delegate " + " Level Loaded ");
        // Debug.Log(scene.name);
        //Debug.Log(mode);
        #endregion

        
        //SoundManager.PlayMusic();
        if(GetSceneName() == "MainMenu")
        {
            PlayerStatus = State.MainMenu;
            UIScreenManager.GetInstance().ShowScreen();
            //GameState = (int)GameStatus.MainMenu;
            LevelIndex = 2;

        }
        else if(GetSceneName() == "3D_GameScene")
        {
            PlayerStatus = State.Waiting;
            //GameState = (int)GameStatus.Waiting;
            LevelIndex = 0;
        }
        // else
        //{
        //  GameManager.State = (int)GameManager.GameState.Playing;
        //}
        Debug.Log(PlayerStatus);
        Debug.Log(LevelIndex);
    }
    #endregion

    #endregion

    #region Game Settings

    
    public State PlayerStatus
    {
        get { return PlayerState; }
        set { PlayerState = value; }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }


    public enum State   // Moved To Game Manager
    {
        MainMenu = 0,
        Waiting = 1,
        Playing = 2,
        GameOver = 3
    }

    public static int GameState;

    public enum GameStatus
    {
        Waiting = 1,
        Playing = 2,
        Pause = 3,
    }

    public static int Game
    {
        get { return GameState; }
        set { GameState = value; }
    }


    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
              //  gapSize = 50f;
                //pipeSpawnTimer = 1.2f;
                break;
            case Difficulty.Medium:
                //gapSize = 40f;
                //pipeSpawnTimer = 1.1f;
                break;
            case Difficulty.Hard:
                //gapSize = 30f;
               // pipeSpawnTimer = 1f;
                break;
            case Difficulty.Impossible:
               // gapSize = 20f;
                //pipeSpawnTimer = .8f;
                break;
        }
    }

    private Difficulty GetDifficulty()
    {
       // if (pipesSpawned >= 30) return Difficulty.Impossible;
       // if (pipesSpawned >= 20) return Difficulty.Hard;
       // if (pipesSpawned >= 10) return Difficulty.Medium;
        return Difficulty.Easy;
    }

    #endregion


}
