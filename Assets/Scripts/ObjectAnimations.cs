using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimations : MonoBehaviour
{
    Animator myani;

    public enum Type { Obstacle, powUp };
    public Type ObjectType;

    private enum State { Idle, Hit, Speed };
    private State ObjectState;

    int animIdle;

    void Start()
    {
        myani = GetComponent<Animator>();
        animIdle = Random.Range(1, 4);
    }

    void Update()
    {
        switch (ObjectType)
        {
            case Type.Obstacle:
                Obstacle();
                break;
            case Type.powUp:
                PowUp();
                break;
            default:
                Debug.LogError("Not valid object type");
                break;
        }
    }

    void Obstacle()
    {
        switch (ObjectState)
        {
            case State.Idle:
                Idle();
                break;
            default:
                Debug.LogError("Not valid object State");
                break;
        }
    }

    void PowUp()
    {
        switch (ObjectState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Speed:
                Speed();
                break;
            default:
                Debug.LogError("Not valid object State");
                break;
        }
    }

    void Idle()
    {
        
        if (ObjectType == Type.Obstacle)
        {
            switch(animIdle)
            {
                case 1:
                    myani.Play("Idle");
                    break;
                case 2:
                    myani.Play("Idle2");
                    break;
                case 3:
                    myani.Play("Idle3");
                    break;
                case 4:
                    myani.Play("Idle4");
                    break;
                default:
                    Debug.LogError("Not valid idle");
                    break;
            }
        }else
        {
            myani.Play("Idle");
        }
    }

    void Hit()
    {
        myani.Play("Hit");
    }

    void Speed()
    {
        myani.Play("Speed");
    }

    void SetState(State newState)
    {
        ObjectState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && ObjectType == Type.powUp)
        {
            SetState(State.Speed);
        }
    }
}
