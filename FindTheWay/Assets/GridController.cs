using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2 GridSize;
    public Node Tile;
    
    public Node[,] Grid;

    public float NodeRadius;
    private float nodeDiameter;

    private int gridSizeX;
    private int gridSizeY;


    // Use this for initialization
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
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var d = GetNodeFromWorldPosition(mousePosition);
            d.CanWalk = false;
            d.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var d = GetNodeFromWorldPosition(mousePosition);
            d.CanWalk = true;
            d.gameObject.SetActive(true);
        }
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

                node.CanWalk = true;
                node.WorldPosition = worldPoint;

                Grid[x, y] = node;
            }
    }

    public Node GetNodeFromWorldPosition(Vector2 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / nodeDiameter);
        int y = Mathf.RoundToInt(worldPosition.y / nodeDiameter);

        return Grid[x, y];
    }

    private void OnDrawGizmos()
    {         
        if (Grid != null)
        {
            foreach (Node node in Grid)
            {
                Gizmos.color = (node.CanWalk) ? Color.green : Color.red;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.3f));
            }
        }
    }
}
