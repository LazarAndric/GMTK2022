using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameOver : MonoBehaviour
{
    public static event Action OnGameOver;
    [SerializeField]
    int idOfEndNode = 33;

    Waypoint lastWaypoint;
    Waypoint[] waypoints;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            lastWaypoint = other.GetComponentInParent<Waypoint>();
            GameOver(GraphBuilder.Graph);
        }
    }
    private void Start()
    {
        waypoints = GameObject.FindObjectsOfType<Waypoint>();

    }

    public bool GameOver(Graph<Waypoint> graph)
    {
        SortedLinkedList<SearchNode<Waypoint>> searchList = new SortedLinkedList<SearchNode<Waypoint>>();
        
        Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> dictonarySearch = new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();
        GraphNode<Waypoint> startNode = graph.Find(lastWaypoint);
        GraphNode<Waypoint> endNode = graph.Find(waypoints[waypoints.Length - idOfEndNode - 1]);

        foreach (GraphNode<Waypoint> waypoint in graph.Nodes)
        {
            SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(waypoint);

            if (waypoint.Value == startNode.Value)
            {
                searchNode.Distance = 0;
            }

            searchList.Add(searchNode);

            dictonarySearch.Add(waypoint, searchNode);
        }
        while (searchList.Count > 0)
        {
            SearchNode<Waypoint> searchNode = searchList.First.Value;
            searchList.Remove(searchNode);
            GraphNode<Waypoint> currentGraphNode = searchNode.GraphNode;
            dictonarySearch.Remove(currentGraphNode);
            if (currentGraphNode.Value == endNode.Value)
            {
                if (searchNode.Distance > 100)
                {
                    OnGameOver?.Invoke();
                    print("Game over");
                    return true;
                }
               
            }
            // For each of the current graph node's neighbors
            foreach (GraphNode<Waypoint> neighbour in currentGraphNode.Neighbors)
            {

                if (dictonarySearch.ContainsKey(neighbour) == true)
                {
                    float distance = searchNode.Distance + 1;
                    SearchNode<Waypoint> neighbourSearchNode = dictonarySearch[neighbour];

                    if (distance < neighbourSearchNode.Distance)
                    {
                        neighbourSearchNode.Distance = distance;
                        neighbourSearchNode.Previous = searchNode;
                        searchList.Reposition(neighbourSearchNode);
                        searchList.ToString();
                    }
                }
            }

        }
        return false;
    }
}