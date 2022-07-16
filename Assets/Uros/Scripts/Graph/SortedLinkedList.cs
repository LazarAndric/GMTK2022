using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sorted linked list
/// </summary>
public class SortedLinkedList<T> : LinkedList<T> where T:IComparable
{
   
    public SortedLinkedList() : base()
    {
    }

    public void Add(T item)
    {
        // add your code here
        if(First == null)
        {
            AddFirst(item);
            return;
        }
        LinkedListNode<T> currentNode = First;

        while(currentNode != null)
        {
            if(currentNode.Value.CompareTo(item) >= 0)
            {
                AddBefore(currentNode, item);
                return;
            }
            else
            {
                if(currentNode.Next == null)
                {
                    AddLast(item);
                    return;
                }
                currentNode = currentNode.Next;
            }
        }
    }


    public void Reposition(T item)
    {
        Remove(item);
        Add(item);
    }

}
