using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBorderDrawer : MonoBehaviour
{
    [SerializeField] SnakeMovement snake;
    [SerializeField] LineRenderer line;
    void Start()
    {
        snake = GameObject.Find("Snake").GetComponent<SnakeMovement>();
        line = GetComponent<LineRenderer>();
        DrawBorder();
    }

    public void DrawBorder()
    {
        line.positionCount = 5;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.SetPosition(0, new Vector3(-snake.borderSizeX - 1.25f, 1,  snake.borderSizeZ + 1));
        line.SetPosition(1, new Vector3( snake.borderSizeX + 1, 1,  snake.borderSizeZ + 1));
        line.SetPosition(2, new Vector3( snake.borderSizeX + 1, 1, -snake.borderSizeZ - 1));
        line.SetPosition(3, new Vector3(-snake.borderSizeX - 1, 1, -snake.borderSizeZ - 1));
        line.SetPosition(4, new Vector3(-snake.borderSizeX - 1, 1,  snake.borderSizeZ + 1));
    } // tamam
}
