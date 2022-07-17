using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHandler : MonoBehaviour
{
    public Vector3 EndPosition;
    // Start is called before the first frame update
    public AnimationHandler Animate;
    public Vector2 StartCordinate;
    public Vector2 Cordinate;
    public float DurationMove;
    public AnimationCurve Curve= new AnimationCurve();
    public bool CanMove=true;
    public Rigidbody Rigidbody;
    private void Start()
    {
        GameHandler.Instance.OnStateChange += onStateChange;

    }
    bool IsDone;
    private void onStateChange(GAMESTATE arg1, GAMESTATE arg2)
    {
        if (GAMESTATE.Gameplay == arg2)
        {
            IsDone = false;
            spawnPlayer();
        }
        if(GAMESTATE.GameOver == arg2)
        {
            IsDone = true;
        }
    }

    [SerializeField]
    float rotationDuration = 0.3f;
    public void spawnPlayer()
    {
        StartCordinate = new Vector2(5,-1);
        Cordinate = StartCordinate;
        if (tryGetPosition(Cordinate, out Vector3 postiion))
            transform.position = postiion;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            GameHandler.Instance.changeState(GAMESTATE.Won);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Animate.startAnimation(AnimationType.Death, onAnimationDone);
            //animation
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Fall"))
        {
            //GameHandler.Instance.removeLife();
            onAnimationDone();
        }
    }
    private void onAnimationDone()
    {
        Rigidbody.useGravity = false;
        Rigidbody.velocity = Vector3.zero;
        Animate.transform.position = Vector3.zero;
        Cordinate = StartCordinate;
        GameHandler.Instance.removeLife(); 
        if (IsDone) return;
        CanMove = true;
        if (tryGetPosition(Cordinate, out Vector3 postiion))
            transform.position = postiion;
        else transform.position = EndPosition;
    }
    // Update is called once per frame
    void Update()
    {

        if (!CanMove) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(tryGetPosition(Cordinate + Vector2.left, out Vector3 position))
            {
                transform.DORotate(Vector3.up * 90f, rotationDuration);
                Cordinate += Vector2.left;
                moveTo(position);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (tryGetPosition(Cordinate + Vector2.right, out Vector3 position))
            {
                transform.DORotate(-Vector3.up * 90f, rotationDuration);
                Cordinate += Vector2.right;
                moveTo(position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (tryGetPosition(Cordinate + Vector2.up, out Vector3 position))
            {
                transform.DORotate(Vector3.up * 180f, rotationDuration);
                Cordinate += Vector2.up;
                moveTo(position);
            }
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if (tryGetPosition(Cordinate + Vector2.down, out Vector3 position))
            {
                transform.DORotate(Vector3.up * 0f, rotationDuration);              
                Cordinate += Vector2.down;
                moveTo(position);
            }
        }
    }
    public void moveTo(Vector3 position)
    {
        transform.DOMove(position, DurationMove).SetEase(Curve).OnComplete(()=>CanMove=true);
    }
    public bool tryGetPosition(Vector2 cordinate, out Vector3 position) => GridHandler.Instance.tryGetPosition(cordinate, out position);
    private void Rotate(float rotation)
    {
        
    }
}
