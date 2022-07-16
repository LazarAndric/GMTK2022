using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class UIHandler : MonoBehaviour
{
    public float speed;
    public TMP_Text TimerLabel;
    public TMP_Text LifeLabel;

    public CanvasGroup UiPanel;
    public CanvasGroup PausePanel;
    public CanvasGroup GameOverPanel;
    private void Start()
    {
        GameHandler.Instance.OnStateChange += onStateChange;
        GameHandler.Instance.subscribeToTimer(onTimerUpdate);
        GameHandler.Instance.OnLifeChange += onLifeChange;
    }

    private void onStateChange(GAMESTATE arg1, GAMESTATE arg2)
    {
        if (arg1 == GAMESTATE.Pause && arg2 == GAMESTATE.Gameplay)
            hidePanel(PausePanel);
        if (arg1==GAMESTATE.Gameplay && arg2==GAMESTATE.Pause)
            showPanel(PausePanel);
        if (arg2 == GAMESTATE.GameOver)
            showPanel(GameOverPanel);
    }

    public void showPanel(CanvasGroup panel)=> panel.DOFade(1, speed);
    public void hidePanel(CanvasGroup panel) => UiPanel.DOFade(0, speed);
    public void onPressPlay()
    {

    }
    public void onGameOverPress()
    {

    }

    private void onLifeChange(int life)
    {
        LifeLabel.text = string.Format("x {0}", (int)life);
    }

    private void onTimerUpdate(float timer)
    {
        var time = DateTime.Parse(timer.ToString());
        TimerLabel.text = string.Format("TIMER: {0}", (int)timer);
    }
}
