using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator Animator;
    public Action OnDone;
    public AnimationType CurrentAnimation;

    public void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void startDeath(AnimationType animationType, Action onDone)
    {
        OnDone= onDone;
        CurrentAnimation = animationType;
        Animator.SetBool(animationType.ToString(), true);
    }
    public void onDone()
    {
        Animator.SetBool(CurrentAnimation.ToString(), false);
        OnDone?.Invoke();
    }
}

public enum AnimationType
{
    Reset,
    Death
}
