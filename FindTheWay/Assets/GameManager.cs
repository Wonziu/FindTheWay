using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridController MyGridController;
    public Text PathLengthText;
    public InputField X, Y;

    void Start()
    {
        SetCameraSizeAndPos();
    }

    public void ShowPathLength()
    {
        PathLengthText.text = "Length: " + Math.Round(MyGridController.GetPathLength(), 1);
    }

    public void DiagonalMovementChange(bool b)
    {
        MyGridController.DiagonalMovement = b;
        MyGridController.MyPathFinding.FindPath(MyGridController.StartPos, MyGridController.EndPos);
    }

    void SetCameraSizeAndPos()
    {
        var position = Camera.main.transform.position;

        position.y = MyGridController.GridSize.y / 2;
        position.x = MyGridController.GridSize.x / 2 - MyGridController.NodeDiameter / 2;

        Camera.main.transform.position = position;

        Camera.main.orthographicSize = MyGridController.GridSize.x / ((float) Screen.width / Screen.height) / 2;

        if (Camera.main.orthographicSize < MyGridController.GridSize.y / 2 + 1f)
            Camera.main.orthographicSize = MyGridController.GridSize.y / 2 + 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetGrid()
    {
        if (X.text != null && Y.text != null && int.Parse(X.text) > 0 && int.Parse(Y.text) > 0)
        {
            MyGridController.GridSize = new Vector2(int.Parse(X.text), int.Parse(Y.text));
            MyGridController.ResetGrid();
            SetCameraSizeAndPos();    
        }
    }
}