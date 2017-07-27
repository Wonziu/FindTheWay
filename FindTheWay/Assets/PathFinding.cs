using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private GridController myGrid;

    private void Awake()
    {
        myGrid = GetComponent<GridController>();
    }

    private void Update()
    {
        FindPath(new Vector2(0, 0), new Vector2(9, 9)); // TODO: 
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = myGrid.GetNodeFromWorldPosition(startPos);
        Node endNode = myGrid.GetNodeFromWorldPosition(endPos);

        List<Node> openSet = new List<Node>();
        List<Node> closeSet = new List<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
                if (openSet[i].fCost < currentNode.fCost)
                    currentNode = openSet[i];

            if (currentNode == endNode)
                RetracePath(startNode, endNode);

            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            foreach (Node neighbour in myGrid.DiagonalMovement ? myGrid.GetNeighboursInSquare(currentNode) : myGrid.GetNeighbours(currentNode))
            {
                if (!neighbour.CanWalk || closeSet.Contains(neighbour))
                    continue;

                var newMovementCost = myGrid.DiagonalMovement
                    ? currentNode.gCost + GetDistanceInDiagonal(currentNode, neighbour)
                    : currentNode.gCost + GetDistance(currentNode, neighbour);

                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;

                    neighbour.hCost = myGrid.DiagonalMovement ? GetDistanceInDiagonal(neighbour, endNode) : GetDistance(neighbour, endNode);

                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        path.Add(startNode);
        myGrid.path = path;
    }


    private int GetDistanceInDiagonal(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);

    }

    private int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        return dstY * 10 + dstX * 10;
    }
}
