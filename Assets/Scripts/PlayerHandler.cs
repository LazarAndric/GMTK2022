using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 Cordinate;
    public float DurationMove;
    public AnimationCurve Curve= new AnimationCurve();
    public bool CanMove=true;
    void Start()
    {
        transform.position= Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(tryGetPosition(Cordinate + Vector2.left, out Vector3 position))
            {
                Cordinate += Vector2.left;
                moveTo(position);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (tryGetPosition(Cordinate + Vector2.right, out Vector3 position))
            {
                Cordinate += Vector2.right;
                moveTo(position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (tryGetPosition(Cordinate + Vector2.up, out Vector3 position))
            {
                Cordinate += Vector2.up;
                moveTo(position);
            }
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if (tryGetPosition(Cordinate + Vector2.down, out Vector3 position))
            {
                Cordinate += Vector2.down;
                moveTo(position);
            }
        }
    }
    public void moveTo(Vector3 position)
    {
        CanMove = false;
        transform.DOMove(position, DurationMove).SetEase(Curve).OnComplete(()=>CanMove=true);
    }
    public bool tryGetPosition(Vector2 cordinate, out Vector3 position) => GridHandler.Instance.tryGetPosition(cordinate, out position);
}
