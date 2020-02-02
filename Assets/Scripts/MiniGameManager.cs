using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject snakeGamePrefab;

    public GameObject activeGame;
    public bool gameStarted;
    public Vector3 oldCameraPos;
    public static MiniGameManager instance;
    public bool partRepaired;
        private void Awake()
    {
        instance = this;
    }
    public void StartFormat()
    {
        UIManager.instance.FormatGame(true);
    }
    void Start()
    {
        gameStarted = false;
        oldCameraPos = Camera.main.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject go = Helper.SendRay();
            if(go != null && go.CompareTag("Part"))
            {
                SlotType miniGame = go.GetComponent<PartData>().type;
                if(go.GetComponent<PartData>().isBroken)
                {
                    if (miniGame == SlotType.GPU)
                    {
                        SnakeGame();
                    }
                    if (miniGame == SlotType.HDD)
                    {
                        StartFormat();
                    }
                }
                //if()
            }
        }
    }

    private void SnakeGame()
    {
        activeGame = Instantiate(snakeGamePrefab);
        activeGame.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 20, 0);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
