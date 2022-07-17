using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public EnemySpawner EnemySpawner;
    public GridHandler GridHandler;
    public GAMESTATE InitState;
    private GAMESTATE CurrentState;
    public Action<GAMESTATE, GAMESTATE> OnStateChange;
    public Action<int> OnLifeChange;
    public float TimerDuration;
    private Timer Timer;
    public static GameHandler Instance;
    public int NumberOfLife;
    public int NumberOfEnemies;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;


        //Prepare All scenes
        GridHandler.initGrid();
        Timer = new Timer();
        Timer.initTimer(TimerDuration, false, false);
        Timer.OnTimerDone += onTimerDone;
        Timer.OnTimerUpdate += onTimerUpdate;
        CheckGameOver.OnGameOver += CheckGameOver_OnGameOver;
        EnemySpawner.spawnEnemies();
        AudioPlayer.Instance.playClip(ClipName.Background, true);

        Life = NumberOfLife;
    }
    public void restartLevel()
    {

    }
    private void CheckGameOver_OnGameOver()
    {
        changeState(GAMESTATE.GameOver);
    }
    int lastLife;
    int Life;
    private void Update()
    {
        if (lastLife != Life)
        {
            lastLife = Life;
            OnLifeChange?.Invoke(Life);
        }
        if (Life == 0) changeState(GAMESTATE.GameOver);
    }
    public int getLife() => Life;
    public void setLife(int life) => Life=life;
    public void addLife() => Life++;
    public void removeLife() => Life--;
    public void subscribeToTimer(Action<float> action) => Timer.OnTimerUpdate+= action;
    public void setTimer(float timer) => Timer.setTimer(timer);
    public void changeTimer(float timer) => Timer.changeTimer(timer);
    public void onTimerUpdate(float time)
    {
        //Timer update
    }
    public void onTimerDone() => changeState(GAMESTATE.GameOver);
    public void startGame()
    {
        Timer.playTimer();
        changeState(GAMESTATE.Gameplay);
    }
    public void changeState(GAMESTATE gameState)
    {
        OnStateChange?.Invoke(CurrentState, gameState);
        CurrentState = gameState;
    }
    public void stopGameplay()
    {

    }
}
[System.Serializable]
public enum GAMESTATE
{
    Start,
    Init,
    Gameplay,
    GameOver,
    Won
}
