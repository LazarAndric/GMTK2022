using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A waypoint
/// </summary>
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    int id;
    void OnTriggerEnter2D(Collider2D other)
    {           
    }

    public Vector3 Position
    {
        get { return transform.position; }
    }

    public int Id
    {
        get { return id;}
        set { id = value;}
    }
}
