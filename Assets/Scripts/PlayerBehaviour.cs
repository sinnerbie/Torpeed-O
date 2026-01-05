using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int poweredQuant = 0;
    public bool powered = false;
    public float powCount = 0.01f;

    public int health = 10;
    public bool invincible = false;
    float inMoment = 1;

    public float playerSpeed = 1.5f;

    [Range(0, 1)]public float acceleration = 0;

    public ParticleSystem explosion;

    Interface hud;
    CameraBehaviour cam;
    AudioSource kaboom;
    ArduinoControls ardCon;
    public AudioSource turbine;

    public bool aoCon = false;
    public int ardPlayDir = 0;

    void Awake()
    {
        ardCon = GetComponent<ArduinoControls>();
    }

    void Start()
    {
        hud = FindFirstObjectByType<Interface>();
        cam = FindFirstObjectByType<CameraBehaviour>();
        kaboom = GetComponent<AudioSource>();
    }

    public void ToggleArduinoControl()
    {
        aoCon = !aoCon;
    }

    void Update()
    {
        if (!aoCon)
        {
            Movement();
            if (powered)
                powCount -= Time.deltaTime;

            if (powCount <= 0)
            {
                poweredQuant--;
                pwrDown();
            }

            if (poweredQuant > 4) poweredQuant = 4;
        } 
        else
        {
            ArdMovement();
            PlayerAcceleration();
        }

        if (health > 10) health = 10;

        if (invincible)
        {
            inMoment -= Time.deltaTime;
        }

        if (inMoment <= 0) invincible = false;
    }

    public void Hurt()
    {
        if (!invincible)
        {
            explosion.Play();
            health -= 2;
            hud.HealthDecrease();
            inMoment = 1;
            invincible = true;
            cam.shake = true;
            kaboom.Play();
        }
    }

    private void Movement()
    {
        if (transform.position.z > 18)
            transform.position = new Vector3(0, 0, 18);

        if (transform.position.z < 0)
            transform.position = new Vector3(0, 0, 0);

        float hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(0, 0, hor * -playerSpeed * Time.deltaTime);
    }

    float delay;
    private void ArdMovement()
    {
        if (transform.position.z > 18)
            transform.position = new Vector3(0, 0, 18);

        if (transform.position.z < 0)
            transform.position = new Vector3(0, 0, 0);

        if (ardCon.leftHand <= 30f && ardCon.rightHand <= 30f)
        {
            if (ardCon.leftHand > ardCon.rightHand + 5)
            {
                float rightForce = map(5, 25, 1, 0, ardCon.rightHand);
                transform.Translate(0, 0, rightForce * playerSpeed * Time.deltaTime);
                ardPlayDir = 1;
            }
            else if (ardCon.rightHand > ardCon.leftHand + 5)
            {
                float leftForce = map(5, 25, 1, 0, ardCon.leftHand);
                transform.Translate(0, 0, leftForce * -playerSpeed * Time.deltaTime);
                ardPlayDir = -1;
            }
            else
            {
                ardPlayDir = 0;
            }
        }
        else
            ardPlayDir = 0;

        if (ardCon.leftHand <= 10f && ardCon.rightHand <= 10f)
        {
            float toAccelerate = ((ardCon.leftHand + ardCon.rightHand) / 2);
            acceleration = map(10, 4, 0, 1, toAccelerate);
        }
        else
            acceleration = 0;

        PlayerAcceleration();
    }

    public void powerUp()
    {
        health += 1;
        hud.HealthIncrease();
        if (!aoCon)
        {
            poweredQuant++;

            switch (poweredQuant)
            {
                case 0:
                    Time.timeScale = 1;
                    powered = false;
                    powCount = 0.01f;
                    turbine.pitch = 0.66f;
                    break;
                case 1:
                    powCount = 10;
                    Time.timeScale = 1.5f;
                    turbine.pitch = 1.07f;
                    powered = true;
                    break;
                case 2:
                    powCount = 10;
                    Time.timeScale = 2;
                    powered = true;
                    turbine.pitch = 1.49f;
                    break;
                case 3:
                    powCount = 10;
                    Time.timeScale = 2.5f;
                    powered = true;
                    turbine.pitch = 1.66f;
                    break;
                case 4:
                    poweredQuant -= 1;
                    powCount = 10;
                    turbine.pitch = 1.66f;
                    break;
                default:
                    Debug.LogError("Not valid state");
                    break;
            }
        }
    }

    public void PlayerAcceleration()
    {
        float scaled = map(0, 1, 1, 2.5f, acceleration);
        Time.timeScale = scaled;
    }

    public float map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void pwrDown()
    {
        switch (poweredQuant)
        {
            case 0:
                Time.timeScale = 1;
                powered = false;
                powCount = 0.01f;
                turbine.pitch = 0.66f;
                break;
            case 1:
                powCount = 10;
                Time.timeScale = 1.5f;
                powered = true;
                turbine.pitch = 1.07f;
                break;
            case 2:
                powCount = 10;
                Time.timeScale = 2;
                powered = true;
                turbine.pitch = 1.49f;
                break;
            case 3:
                powCount = 10;
                Time.timeScale = 2.5f;
                powered = true;
                turbine.pitch = 1.66f;
                break;
            case 4:
                powCount = 10;
                powered = true;
                turbine.pitch = 1.66f;
                break;
            default:
                Debug.LogError("Not valid state");
                break;
        }
    }
}
