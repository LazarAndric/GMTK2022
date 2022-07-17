using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlaneHandler : MonoBehaviour
{
    public AnimationHandler AnimationHandler;
    public AnimationHandler Platofrm;
    public List<FaceControl> Faces = new List<FaceControl>();
    public ColliderControl ColliderControl;
    PlaneBehaviour PlaneBehaviour;
    int RoatationAngle;
    float Duration;
    public GameObject Cube;
    public float WaitTime;
    Timer Timer;
    public int index;
    int iteration;
    bool IsRotationDone;
    public GameObject CurrentObject;
    public void stayInBound()
    {
        if (index >= Faces.Count)
        {
            index = 0;
        }
        else if(index<0)
        {
            index = Faces.Count - 1;
        }    
    }
    public void initializePlane(PlaneBehaviour planeBehaviour, int rotationAngle, float duration, float waitTime, string tagForCollision)
    {
        Timer = new Timer();
        Timer.OnTimerDone += onTimerDone;
        WaitTime = waitTime;
        PlaneBehaviour = planeBehaviour;
        RoatationAngle = rotationAngle;
        Duration = duration;
        ColliderControl.setTag(tagForCollision);
        ColliderControl.TriggerEnter += ColliderControl_TriggerEnter;
        ColliderControl.TriggerExit += ColliderControl_TriggerExit;
        iteration = PlaneBehaviour.PlaneFunctionality.Iteration;
        if (PlaneBehaviour.PlaneType.Type == Type.Surprise)
        {
            Faces[index++].showSpriteFace(PlaneBehaviour.PlaneType.Texture);
            if (iteration > 0)
                Faces[index++].showTextFace(iteration--.ToString());
            else
            {
                if (PlaneBehaviour.PlaneFunctionality.Functionality == Functionality.Empty)
                    Faces[index].hideAll();
                else Faces[index].showSpriteFace(PlaneBehaviour.PlaneFunctionality.Texture);
            }
        }
        else
        {
            for(int i = 0; i < 2; i++)
            {
                if (iteration > 0)
                {
                    Faces[index++].showTextFace(iteration--.ToString());
                }
                else
                {
                    if (PlaneBehaviour.PlaneFunctionality.Functionality == Functionality.Empty)
                        Faces[index].hideAll();
                    else Faces[index].showSpriteFace(PlaneBehaviour.PlaneFunctionality.Texture);
                    iteration--;
                    break;
                }
            }
        }
    }

    private void ColliderControl_TriggerExit()
    {
        CurrentObject = null;
        stopCube();
    }

    private void ColliderControl_TriggerEnter(GameObject obj)
    {
        CurrentObject = obj;
        startCube();
    }
    public void startCube()
    {
        if (IsRotationDone) return;
        if (PlaneBehaviour.PlaneType.Type == Type.Surprise)
        {
            rotateCubeWithCallback(()=>PlaneBehaviour.PlaneFunctionality.OnDone(this));
            IsRotationDone = true;
        }
        else startTimer();
    }
    public void startTimer()
    {
        Timer.startTimer(WaitTime, true, true);
    }
    public void stopCube()
    {
        Timer.interuptTimer();
    }
    public void onTimerDone()
    {
        Timer.pauseTimer();
        rotateCubeWithCallback(
            () =>{
                if (iteration > 0)
                {
                    Faces[index++].showTextFace(iteration--.ToString());
                    Timer.playTimer();
                }
                else if(iteration==0)
                {
                    if (PlaneBehaviour.PlaneFunctionality.Functionality == Functionality.Empty)
                        Faces[index].hideAll();
                    else Faces[index].showSpriteFace(PlaneBehaviour.PlaneFunctionality.Texture);
                    iteration--;
                    Timer.playTimer();
                }
                else if (iteration == -1)
                {
                    Timer.interuptTimer();
                    PlaneBehaviour.PlaneFunctionality.OnDone?.Invoke(this);
                    IsRotationDone = true;
                }
            });
    }
    public void rotateCube()
    {
        Cube.transform.DOBlendableRotateBy(Vector3.right * RoatationAngle, Duration)
            .SetEase(Ease.OutExpo);
    }
    public void rotateCubeWithCallback(Action onDone)
    {
        Cube.transform.DOBlendableRotateBy(Vector3.right * RoatationAngle, Duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() => { onDone?.Invoke(); });
    }

}
[System.Serializable]
public struct PlaneBehaviour
{
    public PlaneFunctionality PlaneFunctionality;
    public PlaneType PlaneType;

    public PlaneBehaviour(PlaneFunctionality planeFunctionality, PlaneType planeType)
    {
        PlaneFunctionality = planeFunctionality;
        PlaneType = planeType;
    }
}
[System.Serializable]
public struct PlaneFunctionality
{
    public Functionality Functionality;
    public Texture Texture;
    public Action<PlaneHandler> OnDone;
    public int Iteration;
    public bool IsDestroy;

    public PlaneFunctionality(Functionality functionality, Texture texture, Action<PlaneHandler> onDone, int iteration, bool isDestroy)
    {
        Functionality = functionality;
        Texture = texture;
        OnDone = onDone;
        Iteration = iteration;
        IsDestroy = isDestroy;
    }
}
[System.Serializable]
public struct PlaneType
{
    public Type Type;
    public Texture Texture;

    public PlaneType(Type type, Texture texture)
    {
        Type = type;
        Texture = texture;
    }
}
public enum Functionality
{
    TimerToDeath,
    Empty,
    Death,
    MoveTo,
    AddLife,
    RemoveLife,
    AddTime,
    RemoveTime,
    SpawnEnemy,
    DestroyEnemy
}
public enum Type
{
    Normal,
    Surprise
}
