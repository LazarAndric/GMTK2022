using System;
using System.Collections.Generic;

public class Graph<T>
{
    List<GraphNode<T>> nodes = new List<GraphNode<T>>();

    public Graph()
    {
    }


    public IList<GraphNode<T>> Nodes
    {
        get { return nodes.AsReadOnly(); }
    }
    public bool AddNode(T value)
    {
        if (Find(value) != null)
        {
            // duplicate value
            return false;
        }
        else
        {
            nodes.Add(new GraphNode<T>(value));
            return true;
        }
    }

    public bool RemoveNode(T value)
    {
        GraphNode<T> removeNode = Find(value);
        if (removeNode == null)
        {
            return false;
        }
        else
        {
            // need to remove as neighor for all nodes
            // in graph
            nodes.Remove(removeNode);
            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveNeighbor(removeNode);
            }
            return true;
        }
    }

    public GraphNode<T> Find(T value)
    {
        foreach (GraphNode<T> node in nodes)
        {
            if (node.Value.Equals(value))
            {
                return node;
            }
        }
        return null;
    }

}