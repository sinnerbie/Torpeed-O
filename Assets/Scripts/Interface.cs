using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public GameObject[] blocks;
    public Image gauge;
    public Text multiplier;

    public float currentTime = 0;
    public bool timerIsRunning = false;
    public Text timeText;

    PlayerBehaviour player;

    public Text finalTime;

    public Button eButton, nButton, hButton;

    GameStateController controller;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerBehaviour>();
        controller = FindFirstObjectByType<GameStateController>();    
    }

    void Start()
    {
        timerIsRunning = true;
        gauge.fillAmount = 0;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }
        
        gauge.fillAmount = player.powCount / 10;

        switch (player.poweredQuant)
        {
            case 0:
                multiplier.text = "";
                break;
            case 1:
                 multiplier.text = "x1";
                break;
            case 2:
                 multiplier.text = "x2";
                break;
            case 3:
                 multiplier.text = "x3";
                break;
            case 4:
                 multiplier.text = "x3";
                break;
            default:
                Debug.LogError("Not valid state");
                break;
        }

        switch (controller.currentDif)
        {
            case GameStateController.Difficulty.Easy:
                eButton.interactable = false;
                nButton.interactable = true;
                hButton.interactable = true;
                break;
            case GameStateController.Difficulty.Normal:
                eButton.interactable = true;
                nButton.interactable = false;
                hButton.interactable = true;
                break;
            case GameStateController.Difficulty.Hard:
                eButton.interactable = true;
                nButton.interactable = true;
                hButton.interactable = false;
                break;
            default:
                Debug.LogError("Not vailid Difficulty");
                break;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:00};{1:00};{2:000}", minutes, seconds, milliSeconds);
    }

    public void HealthIncrease()
    {
        for (int i = 9; i > -1; i--)
        {
            if (i <= player.health - 1)
            {
                blocks[i].SetActive(true);
            }
        }
    }

    public void HealthDecrease()
    {
        for (int i = 9; i > -1; i--)
        {
            if (i > player.health - 1)
            {
                blocks[i].SetActive(false);
            }
        }
    }

    public void DisplayFinalTime()
    {
        float timeToDisplay = currentTime;

        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        finalTime.text = string.Format("{0:00};{1:00};{2:000}", minutes, seconds, milliSeconds);
    }
}
