using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    PlayerBehaviour player;
    public float fov = 47;
    float targetFov;

    private Transform target;
    public Transform[] targets;
    public float speed = 0.25f;

    public float shakeAmount = 0.2f;
	public float decreaseFactor = 1.0f;
    public float shakeDuration = 2;
    public bool shake = false;
	
	Vector3 originalPos;
	
	void OnEnable()
	{
		originalPos = transform.localPosition;
	}

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Update()
    {
        FOV();
        CameraAngle();
        Shake();
    }

    void FOV()
    {
        Camera.main.fieldOfView = fov;
        
        switch (player.poweredQuant)
        {
            case 0:
                targetFov = 47;
                break;
            case 1:
                 targetFov = 53.33f;
                break;
            case 2:
                 targetFov = 59.66f;
                break;
            case 3:
                 targetFov = 66;
                break;
            case 4:
                 targetFov = 66;
                break;
            default:
                Debug.LogError("Not valid state");
                break;
        }

        if (fov != targetFov)
        {
            if (fov > targetFov)
            {
                fov -= Time.deltaTime * 9;
                if (fov < targetFov) fov = targetFov;
            }
            else
            {
                fov += Time.deltaTime * 9;
                if (fov > targetFov) fov = targetFov;
            }
        }
    }

    void CameraAngle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                target = targets[1];
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                target = targets[2];
            }
        }
        else
        {
            target = targets[0];
        }

        Vector3 targetDirection = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);

        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection / 2, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

	void Shake()
	{
        if (shake)
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		        shakeDuration -= Time.deltaTime * decreaseFactor;

            } else {

                shake = false;
                transform.localPosition = originalPos;
                shakeDuration = 0.5f;
            }
        }
	}
}
