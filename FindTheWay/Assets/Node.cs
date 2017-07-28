using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool CanWalk;
    public Vector2 WorldPosition;

    public int gridX;
    public int gridY;

    public Node parent;

    public float gCost;
    public float hCost;

    public void SetProp(bool b, Vector2 v, int x, int y)
    {
        CanWalk = b;
        WorldPosition = v;
        gridX = x;
        gridY = y;
    }

    public float fCost
    {
        get { return gCost + hCost; }
    }
}
