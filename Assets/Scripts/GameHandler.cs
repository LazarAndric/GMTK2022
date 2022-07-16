using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GAMESTATE InitState;
    private GAMESTATE CurrentState;
    public Action<GAMESTATE, GAMESTATE> OnStateChange;
    public Action<int> OnLifeChange;
    public float TimerDuration;
    private Timer Timer;
    public static GameHandler Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Timer = new Timer();
        Timer.initTimer(TimerDuration, false, false);
        Timer.OnTimerDone += onTimerDone;
        Timer.OnTimerUpdate += onTimerUpdate;
        CheckGameOver.OnGameOver += CheckGameOver_OnGameOver;
    }

    private void CheckGameOver_OnGameOver() => changeState(GAMESTATE.GameOver);
    int lastLife;
    int Life;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Timer.playTimer();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            addLife();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            removeLife();
        }
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

    public void changeState(GAMESTATE gameState)
    {
        OnStateChange?.Invoke(CurrentState, gameState);
        CurrentState = gameState;
    }
    public void stopGamePlay()
    {

    }
}
public enum GAMESTATE
{
    Init,
    UI,
    Gameplay,
    Pause,
    GameOver
}
