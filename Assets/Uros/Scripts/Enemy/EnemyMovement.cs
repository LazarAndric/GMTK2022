using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    bool canMove = true;
    Waypoint waypoint;
    Rigidbody rigidbody;
    GraphNode<Waypoint> thisNode;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();  
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Plane"))
        {
            waypoint = other.GetComponentInParent<Waypoint>();
        }

    }
    private void FindTarget()
    {
        int randomNode;
        thisNode = GraphBuilder.Graph.Find(waypoint);
        if(thisNode.Neighbors.Count <= 3)
        {
            randomNode = Random.Range(0, thisNode.Neighbors.Count);
        }
        else
        {
            int random1 = Random.Range(0, thisNode.Neighbors.Count / 2);
            int random2 = Random.Range(thisNode.Neighbors.Count / 2, thisNode.Neighbors.Count);
            int[] rand = { random1, random2 };
            int randIndex = Random.Range(0,2);
            randomNode = rand[randIndex];
            
        }
        
        GoToTarget(randomNode);
    }

    private void GoToTarget(int randomNode)
    {
        Waypoint targetWaypoint = thisNode.Neighbors[randomNode].Value;
        if (targetWaypoint.Occupied == false)
        {
            canMove = false;
            transform.DOMove(targetWaypoint.transform.position, moveSpeed).OnComplete(() => FindTarget());
            targetWaypoint.Occupied = true;
            thisNode.Value.Occupied = false;
        }
        else
        {
            Invoke("FindTarget", 0.02f);
        }
    }

    private void Start()
    {
        FindTarget();
    }
    //private void GoToNextTarget()
    //{
    //    GraphNode<Waypoint> thisNode = GraphBuilder.Graph.Find(waypoint);
    //    print("Za: " + waypoint.Id);
    //    foreach(var nodes in thisNode.Neighbors)
    //    {
    //        print(nodes.Value.Id);
    //    }
    //}
}
