    using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SnakeGameManager : MonoBehaviour, IGameManager
{
    public static SnakeGameManager instance;
    public bool _gameStarted;

    public void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (!_gameStarted && Input.GetMouseButtonDown(0))
        {
            GameStart();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameReset();
        }
    }

    public void GameStart()
    {
        _gameStarted = true;
        SnakeMovement.instance.InitGame();
    }

    public void GameReset()
    {
        _gameStarted = false;
        SnakeMovement.instance.RestartGame();
    }

    public void GameFinish()
    {
        SnakeMovement.instance.FinishGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
       
}