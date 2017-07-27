using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridController MyGridController;

    private void Start()
    {
        SetCameraSize();
    }

    public void DiagonalMovementChange(bool b)
    {
        MyGridController.DiagonalMovement = b;
        MyGridController.MyPathFinding.FindPath(MyGridController.StartPos, MyGridController.EndPos);
    }

    private void SetCameraSize()
    {
        Camera.main.orthographicSize = MyGridController.GridSize.x / ((float) Screen.width / Screen.height) / 2;

        if (Camera.main.orthographicSize < MyGridController.GridSize.y / 2 + 0.5f)
            Camera.main.orthographicSize = MyGridController.GridSize.y / 2 + 0.5f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}