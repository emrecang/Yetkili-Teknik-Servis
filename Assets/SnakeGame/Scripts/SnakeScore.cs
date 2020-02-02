using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeScore : MonoBehaviour
{
    public Text score;
    public int snakeScore;
    public int scoreMul;
    public int specialFoodMul;
    void Start()
    {
        snakeScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //score.text = snakeScore.ToString();
    }
}
