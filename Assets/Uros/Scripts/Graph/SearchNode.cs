using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A node for searching using Dijkstra's algorithm
/// </summary>
/// <typeparam>type for search node</typeparam>
public class SearchNode<T> : IComparable
{


    GraphNode<T> graphNode;
    float distance;
    SearchNode<T> previous;


    public SearchNode(GraphNode<T> graphNode)
    {
        this.graphNode = graphNode;
        distance = float.MaxValue;
        previous = null;
    }

    public GraphNode<T> GraphNode
    {
        get { return graphNode; }
    }

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }


    public SearchNode<T> Previous
    {
        get { return previous; }
        set { previous = value; }
    }

    public int CompareTo(object obj)
    {
        // instance is always greater than null
        if (obj == null)
        {
            return 1;
        }

        // check for correct object type
        SearchNode<T> otherSearchNode = obj as SearchNode<T>;
        if (otherSearchNode != null)
        {
            if (distance < otherSearchNode.Distance)
            {
                return -1;
            }
            else if (distance == otherSearchNode.Distance)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            throw new ArgumentException("Object is not a SearchNode");
        }        
    }

    public override string ToString()
    {
        return distance.ToString();
    }
	
}
