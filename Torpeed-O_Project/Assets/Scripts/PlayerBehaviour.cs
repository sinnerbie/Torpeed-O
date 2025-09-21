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

    public float playerSpeed = 3;

    public ParticleSystem explosion;

    Interface hud;
    CameraBehaviour cam;
    AudioSource kaboom;
    public AudioSource turbine;

    void Start()
    {
        hud = FindObjectOfType<Interface>();
        cam = FindObjectOfType<CameraBehaviour>();
        kaboom = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();

        if (powered)
        {
            powCount -= Time.deltaTime;
        }

        if (powCount <= 0)
        {
            poweredQuant--;
            pwrDown();
        }

        if (poweredQuant > 4) poweredQuant = 4;

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
        float hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(0, 0, hor * -playerSpeed * Time.deltaTime);

        if (transform.position.z > 18)
        {
            transform.position = new Vector3(0, 0, 18);
        }

        if (transform.position.z < 0)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    public void powerUp()
    {
        poweredQuant++;
        health += 1;
        hud.HealthIncrease();
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
