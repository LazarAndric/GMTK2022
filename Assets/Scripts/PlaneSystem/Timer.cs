using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public class Timer 
{
    public event Action<float> OnTimerUpdate;
    public event Action OnTimerDone;
    float Time=0;
    float Duration = 0;
    TweenerCore<float, float, FloatOptions> tweenerCore;
    public void initTimer(float duration, bool isLoop, bool isAsc)
    {
        Duration = duration;
        if (isAsc)
        {
            tweenerCore = DOTween.To(() => Time = 0, x => Time = x, Duration, Duration)
                .OnUpdate(() => OnTimerUpdate?.Invoke(Time))
                .OnStepComplete(() =>OnTimerDone?.Invoke());
        }
        else
        {
            tweenerCore = DOTween.To(() => Time = Duration, x => Time = x, 0, Duration)
                .OnUpdate(() => OnTimerUpdate?.Invoke(Time))
                .OnStepComplete(() => OnTimerDone?.Invoke());
        }
        if (isLoop)
            tweenerCore.SetLoops(-1);
        tweenerCore.Pause();
    }
    public void playTimer() => tweenerCore.Play();
    public void startTimer(float duration, bool isLoop, bool isAsc)
    {
        if (isAsc) tweenerCore = DOTween.To(() => Time=0, x => Time = x, duration, duration)
                .OnUpdate(() => OnTimerUpdate?.Invoke(Time))
                .OnStepComplete(() => OnTimerDone?.Invoke());

        else tweenerCore = DOTween.To(() => Time=duration, x => Time = x, 0, duration)
                .OnUpdate(() => OnTimerUpdate?.Invoke(Time))
                .OnStepComplete(() => OnTimerDone?.Invoke());

        if (isLoop) tweenerCore.SetLoops(-1);
    }
    public void interuptTimer() => tweenerCore.Kill();
    public void pauseTimer() => tweenerCore.Pause();
    public void resumeTimer() => tweenerCore.Play();
    public void changeTimer(float time) => Duration += time;
    public void setTimer(float time) => Duration = time;

}
