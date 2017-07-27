using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Sprite Walkable, Unwalkable;
    public bool DiagonalMovement;
    private Vector2 mousePosition;
    public Vector2 GridSize;
    public Node Tile;
    public List<Node> path;
    public Node[,] Grid;

    public float NodeRadius;
    private float nodeDiameter;

    private int gridSizeX;
    private int gridSizeY;

    void Start()
    {
        nodeDiameter = NodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(GridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridSize.y / nodeDiameter);

        //GenerateTiles();
        GenerateGrid();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwitchTile(false, Unwalkable);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchTile(true, Walkable);
        }
    }

    void SwitchTile(bool b, Sprite s)
    {
        var node = GetNodeFromWorldPosition(mousePosition);
        node.CanWalk = b;
        node.GetComponent<SpriteRenderer>().sprite = s;
    }

    //void GenerateTiles()
    //{
    //    Tiles = new GameObject[gridSizeX, gridSizeY];

    //    for (int x = 0; x < GridSize.x; x++)
    //        for (int y = 0; y < GridSize.y; y++)
    //        {
    //            var o = Instantiate(Tile, new Vector3(x * nodeDiameter + NodeRadius, y * nodeDiameter + NodeRadius, 0), Quaternion.identity);
    //            Tiles[x, y] = o;
    //        }
    //}

    void GenerateGrid()
    {
        Grid = new Node[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = new Vector2(x * nodeDiameter, y * nodeDiameter);

                var node = Instantiate(Tile, worldPoint, Quaternion.identity);
                node.SetProp(true, worldPoint, x, y);

                Grid[x, y] = node;
            }
    }

    public Node GetNodeFromWorldPosition(Vector2 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / nodeDiameter);
        int y = Mathf.RoundToInt(worldPosition.y / nodeDiameter);

        return Grid[x, y];
    }
    public List<Node> GetNeighboursInSquare(Node n)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0 )
                    continue;

                int checkX = n.gridX + x;
                int checkY = n.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(Grid[checkX, checkY]);
            }

        return neighbours;
    }

    public List<Node> GetNeighbours(Node n)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x < 2; x++)
        {
            if (x == 0)
                continue;

            int checkX = n.gridX + x;

            if (checkX >= 0 && checkX < gridSizeX)
                neighbours.Add(Grid[checkX, n.gridY]);
        }

        for (int y = -1; y < 2; y++)
        {
            if (y == 0)
                continue;

            int checkY = n.gridY + y;

            if (checkY >= 0 && checkY < gridSizeY)
                neighbours.Add(Grid[n.gridX, checkY]);
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {         
        if (Grid != null)
        {
            foreach (Node node in Grid)
            {
                Gizmos.color = (node.CanWalk) ? Color.green : Color.red;
                if (path != null)
                {
                    if (path.Contains(node))
                        Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
