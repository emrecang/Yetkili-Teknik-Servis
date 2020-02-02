using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeEatController : MonoBehaviour
{
    public GameObject food;
    public GameObject specialFood;
    public GameObject snakePartPrefab;
    public Text specialText;
    public List<Vector3> eatPos;
    public bool dead;
    public bool eated;
    public bool specialFlag;
    public int eatCount;
    public int specialFoodTimer;
    int specialTimer;
    public int specialFoodSpawnRate;
    SnakeMovement snake;
    public SnakeScore snakeScore;
    public Coroutine coroutine;
    public Coroutine specialFoodCoroutine;
    void Start()
    {
        specialFood.gameObject.SetActive(false);
        dead = false;
        specialFlag = false;
        snake = GetComponent<SnakeMovement>();
        food.transform.position = new Vector3(0, 1, 5);
    }
    void Update()
    {
        SnakeEat();
        if(eatCount >5)
        {
            Destroy(MiniGameManager.instance.activeGame);
            Camera.main.transform.position = MiniGameManager.instance.oldCameraPos;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void SnakeEat()
    {
        if(transform.childCount > 0)
        {
            if (transform.GetChild(0).position == food.transform.position)
            {
                snakeScore.snakeScore += 1 * snakeScore.scoreMul;
                eatPos.Add(food.transform.position);
                eatCount++;
                SnakeFoodRandomSpawner(food);
                eated = true;
                coroutine = StartCoroutine(SnakeScaler());
                if(eatCount % specialFoodSpawnRate == 0 && eatCount != 0)
                { specialFlag = true; }
            }
            if(eatCount % specialFoodSpawnRate == 0 && eatCount != 0 && specialFlag && specialTimer == 0)
            {
                specialTimer = specialFoodTimer;
                specialFlag = false;
                specialFood.gameObject.SetActive(true);
                specialFoodCoroutine = StartCoroutine(SpecialFoodTimer(specialFoodTimer));
                SnakeFoodRandomSpawner(specialFood);
            }
            if(transform.GetChild(0).position == specialFood.transform.position && specialFood.activeInHierarchy)
            {
                //specialText.text = "";
                eatCount++;
                snakeScore.snakeScore += 1 * snakeScore.specialFoodMul*specialTimer;
                specialTimer = 0;
                StopCoroutine(specialFoodCoroutine);
                specialFood.gameObject.SetActive(false);
                eated = true;
                coroutine = StartCoroutine(SnakeScaler());
            }
            
            foreach (var item in eatPos)
            {
                if (eated && transform.GetChild(snake.tailSize - 1).position == item)
                {
                    snake.tailSize += 1;
                    GameObject body = Instantiate(snakePartPrefab, transform);
                    body.transform.position = transform.GetChild(snake.tailSize - 2).position;
                    body.transform.name = "Part" + (transform.childCount - 1);
                    snake.bodyParts.Add(body);
                    eated = false;
                    eatPos.Remove(item);
                    break;
                }
            }
            
        }
        
    }
    public void SnakeFoodRandomSpawner( GameObject myFood)
    {
        if (snake.tailSize < ((snake.borderSizeX * 2) + 1) * ((snake.borderSizeZ * 2) + 1) + 1)
        {
            myFood.transform.position = new Vector3(Random.Range(-snake.borderSizeX, snake.borderSizeX + 1), 
                1, Random.Range(-snake.borderSizeZ, snake.borderSizeZ + 1));

            for (int i = 0; i < snake.bodyParts.Count; i++)
            {
                while (snake.bodyParts[i].transform.position == myFood.transform.position)
                {
                    Debug.Log("Position Changed");
                    SnakeFoodRandomSpawner(myFood);
                }
            }
            if(food.transform.position == specialFood.transform.position)
            {
                myFood.transform.position = new Vector3(Random.Range(-snake.borderSizeX, snake.borderSizeX + 1), 
                    1, Random.Range(-snake.borderSizeZ, snake.borderSizeZ + 1));
            }
        }
        else
        {
            myFood.transform.position = new Vector3(1000, 1000, 1000);
        }
    }
    public IEnumerator SpecialFoodTimer(int timer)
    {
        for (int i = 0; i < timer; i++)
        {
            //specialText.text = specialTimer.ToString();
            if (specialTimer > 0)
                specialTimer--;
            else
                specialTimer = 0;

            yield return new WaitForSeconds(1);
        }
        specialFood.gameObject.SetActive(false);
        //specialText.text = "";
    }
    public IEnumerator SnakeScaler()
    {
        if(eated)
        {
            int a = snake.bodyParts.Count;
           
            for (int i = 0; i < a; i++)
            {
                if (i < snake.bodyParts.Count)
                   snake.bodyParts[i].transform.localScale = new Vector3(1, 1, 1);
                for (int j = 0; j < 100 / snake.snakeSpeed; j++)
                {
                    //yield return null;
                    yield return new WaitForEndOfFrame();
                }
                if (i< snake.bodyParts.Count && !dead)
                    snake.bodyParts[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
    }
    public void CoroutineStopper()
    {
        StopCoroutine(coroutine);
        if(specialFoodCoroutine != null)
            StopCoroutine(specialFoodCoroutine);
        specialTimer = 0;
        specialText.text = "";
        specialFood.SetActive(false);
        snakeScore.snakeScore = 0;
        eatCount = 0;

    }
}
