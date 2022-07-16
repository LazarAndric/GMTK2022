using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
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
    private void FindTargert()
    {
        int randomNode;
        thisNode = GraphBuilder.Graph.Find(waypoint);
        print("nodes " +thisNode.Neighbors.Count);
        if(thisNode.Neighbors.Count <= 3)
        {
            randomNode = Random.Range(0, thisNode.Neighbors.Count);
            print("kranji " + randomNode);
        }
        else
        {
            int random1 = Random.Range(0, thisNode.Neighbors.Count / 2);
            print("1  " + random1);
            int random2 = Random.Range(thisNode.Neighbors.Count / 2, thisNode.Neighbors.Count);
            print("2  " + random2);
            int[] rand = { random1, random2 };
            int randIndex = Random.Range(0,2);
            randomNode = rand[randIndex];
            print("krajnji 2 " + randomNode);
        }
        
        GoToTarget(randomNode);
    }

    private void GoToTarget(int randomNode)
    {
        Waypoint targetWaypoint = thisNode.Neighbors[randomNode].Value;
        print(targetWaypoint.Id);
        canMove = false;
        transform.DOMove(targetWaypoint.transform.position, 1f).OnComplete(() => NextTargert());
    }

    private void NextTargert()
    {
        canMove = true;
        FindTargert();
    }

    private void Start()
    {
        FindTargert();
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
