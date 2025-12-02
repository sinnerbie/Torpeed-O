using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameRules : MonoBehaviour
{
    public GameObject regCard;
    public GameObject goalCard;

    public int totalCards;
    int goalCardLoc;

    public bool playerTurn;
    List<GameObject> cards = new List<GameObject>();
    List<int> cardIDs = new List<int>();
    int infLimit, supLimit;
    int MarkI, MarkS;
    public int selectedCard;

    bool goalLocated = false;
    public Text result;

    int eTurnCount = -1;

    GameStateController controller;

    void Awake()
    {
        controller = FindFirstObjectByType<GameStateController>();
    }

    void Start()
    {
        switch (controller.currentDif)
        {
            case GameStateController.Difficulty.Easy:
                totalCards = 10;
                break;
            case GameStateController.Difficulty.Normal:
                totalCards = 10;
                break;
            case GameStateController.Difficulty.Hard:
                totalCards = 9;
                break;
            default:
                Debug.LogError("Not vailid Difficulty");
                break;
        }
        goalCardLoc = Random.Range(0, totalCards);
        for (int i = 0; i < totalCards; i++)
        {
            if (i == goalCardLoc)
            {
                cards.Add(Instantiate(goalCard, this.transform) as GameObject);
                cardIDs.Add(1);
            }
            else
            {
                cards.Add(Instantiate(regCard, this.transform) as GameObject);
                cardIDs.Add(0);
            }
        }

        supLimit = cards.Count;
        playerTurn = true;
        SelectCard(FindFirstObjectByType<ArduinoControls>().dialPos);
    }

    void Update()
    {
        if (!playerTurn)
        {
            EnemyTurn();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!cards[selectedCard].GetComponent<CardProperties>().beenClicked)
                {
                    cards[selectedCard].GetComponent<CardProperties>().Clicked();
                }
            }
        }
    }

    void EnemyTurn()
    {
        if (!goalLocated)
        {
            switch (controller.currentDif)
            {
                case GameStateController.Difficulty.Easy:
                    Invoke("EasyAI", 1);
                    break;
                case GameStateController.Difficulty.Normal:
                    Invoke("NormAI", 1);
                    break;
                case GameStateController.Difficulty.Hard:
                    Invoke("SummonHardAI", 1);
                    break;
                default:
                    Debug.LogError("Not vailid Difficulty");
                    break;
            }
        }
    }

    void EasyAI()
    {
        eTurnCount++;
        if (cards[eTurnCount].GetComponent<CardProperties>().beenClicked == false)
        {
            cards[eTurnCount].GetComponent<CardProperties>().Clicked();
            CancelInvoke("EasyAI");
        }
        else
        {
            return;
        }
    }

    void NormAI()
    {
        if (infLimit < supLimit)
        {
            int mid = (supLimit + infLimit) / 2;
            if (cards[mid].GetComponent<CardProperties>().beenClicked == false)
            {
                if (mid > goalCardLoc)
                {
                    cards[mid].GetComponent<CardProperties>().Clicked();
                    supLimit = mid + 1;
                    CancelInvoke("NormAI");
                }
                if (mid < goalCardLoc) 
                {
                    cards[mid].GetComponent<CardProperties>().Clicked();
                    infLimit = mid - 1;
                    CancelInvoke("NormAI");
                }
                if (mid == goalCardLoc)
                {
                    cards[mid].GetComponent<CardProperties>().Clicked();
                    CancelInvoke("NormAI");
                }
            }
            else
            {
                if (mid > goalCardLoc) supLimit = mid + 1;
                if (mid < goalCardLoc) infLimit = mid - 1;
                return;
            }
        }
    }

    void SummonHardAI()
    {
        cards[HardAI(infLimit, supLimit)].GetComponent<CardProperties>().Clicked();
        CancelInvoke("SummonHardAI");
    }

    int HardAI(int LimI, int LimS)
    {
        if (LimI < LimS)
        {
            MarkI = LimI + (LimS - LimI) / 3;
            MarkS = LimS + (LimS - LimI) / 3;
            if (MarkI == goalCardLoc)
            { 
                return MarkI;
            }
            if (MarkS == goalCardLoc) 
            {
                return MarkS;
            }
            if (goalCardLoc < MarkI)
            {
                supLimit = MarkI - 1;
                return MarkI;
            }
            if (goalCardLoc > MarkS)
            {
                infLimit = MarkS + 1;
                return MarkS;
            }
            else if (goalCardLoc > MarkI && goalCardLoc < MarkS)
            {
                return HardAI(MarkI + 1,  MarkS - 1);
            }
            return 0;
        }
        return 0;
    }

    public void ShiftTurn()
    {
        playerTurn = !playerTurn;
    }

    public void SelectCard(int dial)
    {
        Debug.Log("Changing selection");
        int newCard = map(0, 1024, 0, totalCards - 1, dial);
        selectedCard = newCard;

        for (int i = 0; i < totalCards; i++)
        {
            if (i == selectedCard)
                cards[i].GetComponent<CardProperties>().CheckSelected(true);
            else
                cards[i].GetComponent<CardProperties>().CheckSelected(false);
        }
    }

    public void GoalFound()
    {
        goalLocated = true;

        if (playerTurn)
        {
            Invoke("Victory", 1);
            controller.Invoke("ContinueGame", 3);
        }
        else
        {
            Invoke("Failure", 1);
            controller.Invoke("GoToStart", 3);
        }
    }

    void Victory()
    {
        result.text = "You won!";
    }

    void Failure()
    {
        result.text = "You lost!";
    }

    public int map(int OldMin, int OldMax, int NewMin, int NewMax, int OldValue)
    {

        int OldRange = (OldMax - OldMin);
        int NewRange = (NewMax - NewMin);
        int NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
