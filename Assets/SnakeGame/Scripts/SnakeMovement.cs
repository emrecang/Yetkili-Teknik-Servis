using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public static SnakeMovement instance;
    public float insSnakeSpeed;
    public int insSnakeTailSize;
    public int borderSizeX;
    public int borderSizeZ;
    [HideInInspector] public float snakeSpeed;
    [HideInInspector] public int tailSize;
    [HideInInspector] public Vector3 nextPos;
    [HideInInspector] public Vector3 velocity;
    
    public List<Vector3> trail = new List<Vector3>();
    public List<Vector3> firstPosTrail = new List<Vector3>();
    public List<GameObject> bodyParts = new List<GameObject>();

    public SnakeEatController snake;
    public SnakeController snakeController;
    Coroutine coroutine;
    private void Awake()
    {
        tailSize = insSnakeTailSize;
       
        instance = this;
        snake = GetComponent<SnakeEatController>();
        snakeController = GetComponent<SnakeController>();
        SnakeCreatePart();
    }
    #region Movement
    public IEnumerator MoveSnake()
    {
        while(true)
        {
            nextPos += velocity;
            transform.GetChild(0).position = nextPos;
            SnakeTeleport();
            transform.GetChild(0).position = nextPos;
            SnakePartsMove();
            EatSelfCheck();
            for (int i = 0; i < 100 / snakeSpeed; i++)
            {
                //yield return null;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void SnakeTeleport()
    {
        if(transform.GetChild(0).position.x > borderSizeX)
            nextPos = new Vector3(-borderSizeX, 1, transform.GetChild(0).position.z);

        if (transform.GetChild(0).position.x < -borderSizeX)
           nextPos = new Vector3(borderSizeX, 1, transform.GetChild(0).position.z);

        if(transform.GetChild(0).position.z > borderSizeZ)
            nextPos = new Vector3(transform.GetChild(0).position.x, 1, -borderSizeZ);

        if(transform.GetChild(0).position.z < -borderSizeZ)
            nextPos = new Vector3(transform.GetChild(0).position.x, 1, borderSizeZ);
    } // tamam
    public void SnakePartsMove() // tamam
    {
        trail.Insert(0 , nextPos);
        while(trail.Count > tailSize)
        {
            trail.RemoveAt(trail.Count - 1);
        }
        int i = 0;
        foreach (Vector3 ch in trail)
        {
            transform.GetChild(i).position = trail[i];
            i++;
        }
    }
    #endregion

    public void SnakeCreatePart() // tamam.
    {
        for (int i = 0; i < tailSize; i++)
        {
            GameObject body = Instantiate(snake.snakePartPrefab, transform);
            body.transform.position = new Vector3(0, 1, -i);
            body.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            body.transform.name = "Part" + i;
            bodyParts.Add(body);
        }
    }
    public void EatSelfCheck()
    {
        for (int i = 1; i < trail.Count; i++)
        {
            if (transform.GetChild(0).position == trail[i])
            {
                snake.dead = true;
                snake.eated = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
    public void InitGame()
    {
        tailSize = insSnakeTailSize;
        Debug.Log(transform.childCount);
        if (transform.childCount < 1)
        {
            SnakeCreatePart();
        }
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (firstPosTrail.Count < tailSize)
                firstPosTrail.Add(bodyParts[i].transform.position);
            else
                return;
        }
        for (int i = 0; i < bodyParts.Count; i++)
        {
           transform.GetChild(i).transform.position = firstPosTrail[i];
            transform.GetChild(i).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        snake.dead = false;
        nextPos = new Vector3(0, 1, 0);
        velocity = new Vector3(0, 0, 1);
        snake.food.transform.position = new Vector3(0, 1, 5);
        snakeSpeed = insSnakeSpeed;
        transform.position = nextPos;
        transform.GetChild(0).position = nextPos;
        trail = new List<Vector3>(firstPosTrail);
        snakeController.dir = SnakeController.Direction.Up;
        coroutine = StartCoroutine(MoveSnake());
    }

    public void RestartGame()
    {
        FinishGame();
        InitGame();
    }
    public void FinishGame()
    {
        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
            snake.CoroutineStopper();
        }
        int a = transform.childCount;
        for (int i = 0; i < a; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        bodyParts.Clear();
        trail.Clear();
        firstPosTrail.Clear();
    }
}
