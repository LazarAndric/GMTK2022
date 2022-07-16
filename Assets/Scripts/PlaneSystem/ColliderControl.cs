using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    public event Action<GameObject> TriggerEnter;
    public event Action TriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke();
    }
}
