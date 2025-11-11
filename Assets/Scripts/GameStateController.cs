using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum State { Start, Play, Over };
    public State CurrentState;

    public enum Difficulty { Easy, Normal, Hard };
    public Difficulty currentDif;

    public GameObject StartUI, PlayUI, OverUI, clearObst, MinigameScreen;
    private GameObject minScree;
    public Animator difBar;

    Interface face;
    PlayerBehaviour player;

    bool difVisible;

    void Start()
    {
        CurrentState = State.Start;
        face = FindFirstObjectByType<Interface>();
        player = FindFirstObjectByType<PlayerBehaviour>();
        difVisible = false;
        currentDif = Difficulty.Normal;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case State.Start:
                sUI();
                break;
            case State.Play:
                pUI();
                break;
            case State.Over:
                oUI();
                break;
            default:
                Debug.LogError("Not valid game state");
                break;
        }
    }

    void sUI()
    {
        PlayUI.SetActive(false);
        OverUI.SetActive(false);
        StartUI.SetActive(true);
        clearObst.SetActive(true);
    }

    void pUI()
    {
        PlayUI.SetActive(true);
        OverUI.SetActive(false);
        StartUI.SetActive(false);
        clearObst.SetActive(false);
        if (player.health <= 0) StateShift(State.Over);
    }

    void oUI()
    {
        PlayUI.SetActive(false);
        OverUI.SetActive(true);
        StartUI.SetActive(false);
        clearObst.SetActive(true);
        face.timerIsRunning = false;
        face.DisplayFinalTime();
    }

    public void StateShift(State newState)
    {
        CurrentState = newState;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToStart()
    {
        Destroy(minScree);
        StateShift(State.Start);
    }

    public void PlayGame()
    {
        StateShift(State.Play);
        face.timerIsRunning = true;
        face.currentTime = -1;
        player.health = 10;
        player.acceleration = 0;
        /*player.poweredQuant = 0;
        player.powCount = 0.01f;
        player.pwrDown();*/
        face.HealthIncrease();
    }

    public void StartMinigame()
    {
        Instantiate(MinigameScreen, OverUI.transform);
        minScree = GameObject.Find("MinigameScreen(Clone)");
    }

    public void ContinueGame()
    {
        Destroy(minScree);
        StateShift(State.Play);
        face.timerIsRunning = true;
        player.health = 10;
        player.acceleration = 0;
        /*player.poweredQuant = 0;
        player.powCount = 0.01f;
        player.pwrDown();*/
        face.HealthIncrease();
    }

    public void DifOptions()
    {
        difVisible = !difVisible;
        if (difVisible)
            difBar.Play("LoadIn");
        else
            difBar.Play("LoadOut");
    }

    public void ShiftEasy()
    {
        currentDif = Difficulty.Easy;
        difVisible = false;
        difBar.Play("LoadOut");
    }
    public void ShiftNorm()
    {
        currentDif = Difficulty.Normal;
        difVisible = false;
        difBar.Play("LoadOut");
    }
    public void ShiftHard()
    {
        currentDif = Difficulty.Hard;
        difVisible = false;
        difBar.Play("LoadOut");
    }
}
