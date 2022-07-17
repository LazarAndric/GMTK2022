using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameOver : MonoBehaviour
{

    public static event Action OnGameOver;

    int idOfEndNode = -1;
    [HideInInspector]
    public static List<Waypoint> waypointsList;

    static int idOfEndWaypoint;
    static Waypoint lastWaypoint;
    public Waypoint[] waypoints;

    static Waypoint waypointNextToFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            lastWaypoint = other.GetComponentInParent<Waypoint>();
        }
    }
    private void Start()
    {
        waypointsList = new List<Waypoint>();
        idOfEndWaypoint = idOfEndNode;
        waypoints = GameObject.FindObjectsOfType<Waypoint>();
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointsList.Add(waypoints[i]);
        }
        foreach (Waypoint way in waypointsList)
        {
            if (way.Id == idOfEndWaypoint)
            {
                waypointNextToFinish = way;
                break;
            }
        }
    }

    public static void GameOver(Graph<Waypoint> graph, Waypoint w)
    {
        
        waypointsList.Remove(w);
        SortedLinkedList<SearchNode<Waypoint>> searchList = new SortedLinkedList<SearchNode<Waypoint>>();
        
        Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> dictonarySearch = new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();
        GraphNode<Waypoint> startNode = graph.Find(lastWaypoint);

        GraphNode <Waypoint> endNode = graph.Find(waypointNextToFinish);
        // if there is 1 end node and it is destroyed it is game over
        if(endNode == null)
        {
            OnGameOver?.Invoke();
            print("Game over, path can't be found");
            return;
        }

        foreach (GraphNode<Waypoint> waypoint in graph.Nodes)
        {
            SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(waypoint);

       

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
                // if distance is anything above 36 (number of ndoes), path doesn't exists
                if (searchNode.Distance > 100)
                {
                    OnGameOver?.Invoke();
                    print("Game over, path can't be found");
                    return;
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
       
    }
}