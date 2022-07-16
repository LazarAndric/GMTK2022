using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    public event Action<GameObject> TriggerEnter;
    public event Action TriggerExit;
    string CollisionTag;
    public void setTag(string tag)
    {
        CollisionTag = tag;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(CollisionTag)) return;
        TriggerEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(CollisionTag)) return;
        TriggerExit?.Invoke();
    }
}
