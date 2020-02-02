using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    SnakeMovement snake;
    Vector3 firstTouchPos;
    Vector3 lastTouchPos;
    public float swipeOffset;
    bool fingerUp;
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }; // tamam

    public Direction dir;
    public void Start()
    {
        snake = transform.GetComponent<SnakeMovement>();
        dir = Direction.Up;
        swipeOffset = 0.6f;
        fingerUp = false;
    }
    public void Update()
    {
        SwipeControl();
    }
    public void SwipeControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fingerUp)
            {
                firstTouchPos = SendRay();
                fingerUp = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            lastTouchPos = SendRay();
            if(!fingerUp)
            {
                Vector3 diff = firstTouchPos - lastTouchPos;

                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z)) // Horizantal
                {
                    if (diff.x > swipeOffset && dir != Direction.Right) //left
                    {
                        dir = Direction.Left;
                        snake.velocity = new Vector3(-1, 0, 0);
                        fingerUp = true;
                    }
                    else if (diff.x < -swipeOffset && dir != Direction.Left) //right
                    {
                        dir = Direction.Right;
                        snake.velocity = new Vector3(1, 0, 0);
                        fingerUp = true;
                    }
                }
                else //Vertical
                {
                    if (diff.z > swipeOffset && dir != Direction.Up) //down
                    {
                        dir = Direction.Down;
                        snake.velocity = new Vector3(0, 0, -1);
                        fingerUp = true;
                    }
                    else if (diff.z < -swipeOffset && dir != Direction.Down) //up
                    {
                        dir = Direction.Up;
                        snake.velocity = new Vector3(0, 0, 1);
                        fingerUp = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            fingerUp = true;  
        }
    } // tamam
    public Vector3 SendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    } // tamam

    
}
