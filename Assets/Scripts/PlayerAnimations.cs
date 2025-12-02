using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Transform target;
    public Transform[] targets;
    public float speed = 2;

    ArduinoControls ardCon;
    PlayerBehaviour player;

    void Awake()
    {
        ardCon = GetComponentInParent<ArduinoControls>();
        player = GetComponentInParent<PlayerBehaviour>();
    }

    void Update()
    {
        if (!player.aoCon)
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
        }
        else
        {
            if (player.ardPlayDir != 0)
            {
                if (player.ardPlayDir > 0)
                    target = targets[1];
                else
                    target = targets[2];
            }
            else
                target = targets[0];
        }

        Vector3 targetDirection = target.position - transform.position;

        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void Hello()
    {
        Debug.Log("Event works");
    }
}
