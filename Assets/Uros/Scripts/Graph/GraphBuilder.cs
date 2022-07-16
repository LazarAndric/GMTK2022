using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphBuilder :MonoBehaviour
{
    static Graph<Waypoint> graph;


    public static void CreateGraph(List<Waypoint> waypoints, float offsetX, float offsetY)
    {
        // add nodes (all waypoints, including start and end) to graph
        graph = new Graph<Waypoint>();
        //GameObject[] gameWaypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        //Waypoint[] waypoints = new Waypoint[gameWaypoint.Length];

        //see what will happen with start and end

        //Waypoint start = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
        //Waypoint end = GameObject.FindGameObjectWithTag("End").GetComponent<Waypoint>();
        //graph.AddNode(start);
        for (int i = 0; i < waypoints.Count; i++)
        {
            //waypoints[i] = gameWaypoint[i].GetComponent<Waypoint>();
            graph.AddNode(waypoints[i]);
        }

        //graph.AddNode(end);
        List<float> vertices = new List<float>();

        // add neighbors for each node in graph
        foreach (GraphNode<Waypoint> graphNode in graph.Nodes)
        {
            foreach (GraphNode<Waypoint> neighbour in graph.Nodes)
            {
                if (neighbour == graphNode)
                    continue;
                // if node is 1 place from current node and not diagonal then add
                if ((Mathf.Abs(neighbour.Value.Position.z - graphNode.Value.Position.z) <= 1 + offsetY && Mathf.Abs(neighbour.Value.Position.x - graphNode.Value.Position.x) == 0)
                   || Mathf.Abs(neighbour.Value.Position.z - graphNode.Value.Position.z) == 0 && Mathf.Abs(neighbour.Value.Position.x - graphNode.Value.Position.x) <= 1 + offsetX)
                {
                    graphNode.AddNeighbor(neighbour, 1);
                }

            }
        }
    }

    public static Graph<Waypoint> Graph
    {
        get { return graph; }
        set { graph = value; }
    }
}
