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
    public CanvasGroup GameOverPanel;
    public CanvasGroup WonPanel;
    private void Start()
    {
        GameHandler.Instance.OnStateChange += onStateChange;
        GameHandler.Instance.subscribeToTimer(onTimerUpdate);
        GameHandler.Instance.OnLifeChange += onLifeChange;
        onLifeChange(GameHandler.Instance.getLife());
    }

    private void onStateChange(GAMESTATE arg1, GAMESTATE arg2)
    {
        if (arg2 == GAMESTATE.GameOver)
            showPanel(GameOverPanel);
        if (arg2 == GAMESTATE.Won)
            showPanel(WonPanel);
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
        LifeLabel.text = string.Format("x {0}", life);
    }

    private void onTimerUpdate(float timer)
    {
        TimerLabel.text = string.Format("TIMER: {0}", (int)timer);
    }
}
