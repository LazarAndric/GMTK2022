using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

public class Timer 
{
    float Time=0;
    TweenerCore<float, float, FloatOptions> tweenerCore;
    public void startTimer(Action onUpdate, Action onDone, float duration, bool isLoop, bool isAsc)
    {
        if (isAsc)
        {
            tweenerCore = DOTween.To(() => Time=0, x => Time = x, duration, duration)
                .OnUpdate(() => onUpdate?.Invoke())
                .OnStepComplete(() => { onDone?.Invoke(); });
        }
        else
        {
            tweenerCore = DOTween.To(() => Time=duration, x => Time = x, 0, duration)
                .OnStepComplete(() => onDone());
        }
        if (isLoop)
            tweenerCore.SetLoops(-1);
    }
    public void interuptTimer()
    {
        tweenerCore.Kill();
    }
    public void pauseTimer()
    {
        tweenerCore.Pause();
    }
    public void resumeTimer()
    {
        tweenerCore.Play();
    }
}
