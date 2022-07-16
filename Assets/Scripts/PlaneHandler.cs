using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlaneHandler : MonoBehaviour
{
    public int RoatationAngle;
    public float Duration;
    public GameObject Cube;
    public bool IsRotated;
    private void OnTriggerEnter(Collider other)
    {
        IsRotated = false;
        startRotateCube();
    }
    private void OnTriggerExit(Collider other)
    {
    }

    public void startRotateCube()
    {
        if (!IsRotated)
        {
            Cube.transform.DOBlendableRotateBy(Vector3.right * RoatationAngle, Duration).SetEase(Ease.OutExpo);
            IsRotated= true;
        }
    }

}
