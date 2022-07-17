using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator Animator;
    private Action OnDone;
    private AnimationType CurrentAnimation;

    public void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void startAnimation(AnimationType animationType, Action onDone)
    {
        Animator.SetBool("Reset", false);
        Animator.SetBool(CurrentAnimation.ToString(), false);
        OnDone = onDone;
        CurrentAnimation = animationType;
        Animator.SetBool(animationType.ToString(), true);
    }
    void onDone()
    {
        Animator.SetBool(CurrentAnimation.ToString(), false);
        Animator.SetBool("Reset", true);
        OnDone?.Invoke();
    }
}

public enum AnimationType
{
    Death
}
