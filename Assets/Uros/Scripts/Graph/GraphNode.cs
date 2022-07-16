using System;
using System.Collections.Generic;


public class GraphNode<T>
{
    T value;
    List<GraphNode<T>> neighbors;
    List<float> weights;

    public GraphNode(T value)
    {
        this.value = value;
        neighbors = new List<GraphNode<T>>();
        weights = new List<float>();
    }


    public T Value
    {
        get { return value; }
    }

    public IList<GraphNode<T>> Neighbors
    {
        get { return neighbors.AsReadOnly(); }
    }



    public bool AddNeighbor(GraphNode<T> neighbor, float weight)
    {
        // don't add duplicate nodes
        if (neighbors.Contains(neighbor))
        {
            return false;
        }
        else
        {
            neighbors.Add(neighbor);
            weights.Add(weight);
            return true;
        }
    }

    public float GetEdgeWeight(GraphNode<T> neighbor)
    {
        // make sure edge exists
        if (!neighbors.Contains(neighbor))
        {
            throw new InvalidOperationException("Trying to retrieve weight of non-existent edge");
        }
        else
        {
            int index = neighbors.IndexOf(neighbor);
            return weights[index];
        }
    }

    public bool RemoveNeighbor(GraphNode<T> neighbor)
    {
        // remove weight for neighbor
        int index = neighbors.IndexOf(neighbor);
        if (index == -1)
        {
            // neighbor not in list
            return false;
        }
        else
        {
            // remove neighbor and edge weight
            neighbors.RemoveAt(index);
            weights.RemoveAt(index);
            return true;
        }
    }
}
