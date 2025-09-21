using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    float speed = 20;
    
    PlayerBehaviour player;

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            player.Hurt();
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
