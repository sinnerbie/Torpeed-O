using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour
{
    public bool isGoal;
    public bool beenClicked = false;

    MinigameRules MgRl;
    Animator myAni;
    AudioSource myAud;

    void Awake()
    {
        MgRl = FindFirstObjectByType<MinigameRules>();
        myAni = GetComponent<Animator>();
        myAud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (MgRl.playerTurn)
        {
            if (!beenClicked) GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void Clicked()
    {
        beenClicked = true;
        GetComponent<Button>().interactable = false;
        myAni.Play("Reveal");
        myAud.Play();
        if (isGoal)
        {
            MgRl.GoalFound();
        }
        else
        {
            MgRl.ShiftTurn();
        }
    }
}
