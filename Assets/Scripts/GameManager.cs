using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance; //singleton

    public delegate void thisDelegate();
    public event thisDelegate notify;

    [SerializeField] GameObject[] homes;

    int points = 0;

    GameState currentGameState;

    public enum GameState
    {
        GameOver,
        Restart,
        Play,
        Pause,
        PreGame
    }
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        this.UpdateGameState(GameState.PreGame);
        InputController.SharedInstance.notifyEsc += OnPauseButton;

        foreach (GameObject home in homes)
            home.GetComponent<Home>().notify += OnHomeDestroy;   
    }

    void UpdateGameState(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.GameOver:
                currentGameState = GameState.GameOver;
                this.GameOver();
                break;
            case GameState.Pause:
                currentGameState = GameState.Pause;
                this.Pause();
                break;
            case GameState.Play:
                currentGameState = GameState.Play;
                this.Play();
                break;
            case GameState.Restart:
                currentGameState = GameState.Restart;
                this.Restart();
                break;
            case GameState.PreGame:
                currentGameState = GameState.PreGame;
                this.PreGame();
                break;
        }
    }

    void OnHomeDestroy(GameObject homeobj)
    {
        //check loose condition, is any home alive
        bool isAlive = false;
        foreach (GameObject home in homes)
        {
            if (home.activeInHierarchy) isAlive = true;
        }

        if (isAlive == false) UpdateGameState(GameState.GameOver);
    }

    void Pause()
    {
        //time freeze
        GlobalSettings.SetTimeSpeed(0f);
        //update UI
        this.GetComponent<UIManager>().UI_Pause();
    }
    void Play()
    {
        //time freeze
        GlobalSettings.SetTimeSpeed();
        //update UI
        this.GetComponent<UIManager>().UI_Play();
    }

    void GameOver()
    {
        //time freeze
        GlobalSettings.SetTimeSpeed(0f);
        //update UI
        this.GetComponent<UIManager>().UI_Gameover();
    }

    void Restart()
    {
        this.initStartValues();
        UpdateGameState(GameState.Play);
    }
    
    void PreGame()
    {
        this.initStartValues();
        //time freeze
        GlobalSettings.SetTimeSpeed(0f);
        //update UI
        this.GetComponent<UIManager>().UI_PreGame();

        GlobalSettings.SetTimeSpeed(0f);
    }

    //when meteor shot down by player
    public void OnMeteorShotDown()
    {
        addPoints(10);
    }

    //increse score
    public void addPoints(int pts) { 
        this.points += pts;
        this.GetComponent<UIManager>().UpdatePoints(this.points);
    }
    public int getPoints() { return this.points; }

    //set values for start/restart game
    void initStartValues()
    {
        //set points
        this.points = 0;
        this.GetComponent<UIManager>().UpdatePoints(this.points);
        //set homes
        foreach (GameObject home in homes)
            home.SetActive(true);
        //set pools (playerManager/enemyManager)
        if(notify != null) notify();

        //set time
        GlobalSettings.SetTimeSpeed(0f);
    }

    void OnPauseButton()
    {
        if (currentGameState == GameState.Play)
            UpdateGameState(GameState.Pause);
        else if (currentGameState == GameState.Pause)
            UpdateGameState(GameState.Play);
        else if (currentGameState == GameState.GameOver)
            UpdateGameState(GameState.Restart);
        else if (currentGameState == GameState.PreGame)
            UpdateGameState(GameState.Play);
    }
}
