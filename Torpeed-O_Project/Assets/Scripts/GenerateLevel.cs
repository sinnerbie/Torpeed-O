using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wall;
    public GameObject spawner;
    public GameObject powerUp;

    public int width = 3;
    public int height = 1;

    public float scale = 20f;
    float offsetX = 100;
    float offsetY = 100;

    float countdown = 2f;

    GameStateController controller;

    void Start()
    {
        offsetX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);
        controller = FindObjectOfType<GameStateController>();
        GenerateObstacles();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            switch (controller.currentDif)
            {
                case GameStateController.Difficulty.Easy:
                    EasyObstacles();
                    break;
                case GameStateController.Difficulty.Normal:
                    GenerateObstacles();
                    break;
                case GameStateController.Difficulty.Hard:
                    HardObstacles();
                    break;
                default:
                    Debug.LogError("Not vailid Difficulty");
                    break;
            }
            
            countdown = Random.Range(0.5f, 3);
        }
    }

    void EasyObstacles()
    {
        offsetX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float onGrid = CalculateValue(x, y);
                if (onGrid < 0.4f)
                {
                    Instantiate(wall, new Vector3(64, 0, x), Quaternion.identity);
                }
                else if (onGrid >= 0.4f)
                {
                    int chance = Random.Range(0, 100);
                    if (chance < 5)
                    {
                        Instantiate(spawner, new Vector3(64, 0, x), Quaternion.identity);
                    }
                    else if (chance > 30)
                    {
                        Instantiate(powerUp, new Vector3(64, 0, x), Quaternion.identity);
                    }
                }
            }
        }
    }

    void GenerateObstacles()
    {
        offsetX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float onGrid = CalculateValue(x, y);
                if (onGrid < 0.4f)
                {
                    Instantiate(wall, new Vector3(64, 0, x), Quaternion.identity);
                }else if(onGrid >= 0.4f)
                {
                    int chance = Random.Range(0, 100);
                    if(chance < 5)
                    {
                        Instantiate(spawner, new Vector3(64, 0, x), Quaternion.identity);
                    }
                    else if(chance > 95)
                    {
                        Instantiate(powerUp, new Vector3(64, 0, x), Quaternion.identity);
                    }
                }
            }
        }
    }

    void HardObstacles()
    {
        offsetX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float onGrid = CalculateValue(x, y);
                if (onGrid < 0.4f)
                {
                    Instantiate(wall, new Vector3(64, 0, x), Quaternion.identity);
                }
                else if (onGrid >= 0.4f)
                {
                    int chance = Random.Range(0, 100);
                    if (chance < 50)
                    {
                        Instantiate(spawner, new Vector3(64, 0, x), Quaternion.identity);
                    }
                    else if (chance >= 98)
                    {
                        Instantiate(powerUp, new Vector3(64, 0, x), Quaternion.identity);
                    }
                }
            }
        }
    }

    float CalculateValue(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float value = Mathf.PerlinNoise(xCoord, yCoord);
        return value;
    }
}
