using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    GenerateTerrain terGen;

    PlayerBehaviour player;

    [SerializeField] string obType;

    AudioSource chime;

    void Awake()
    {
        terGen = FindFirstObjectByType<GenerateTerrain>();
        player = FindFirstObjectByType<PlayerBehaviour>();

        if (obType == "powUp") chime = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(-terGen.speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (obType == "wall" || obType == "spawner")
            {
                player.Hurt();
                Destroy(gameObject);
            }
            else if (obType == "powUp" && player.poweredQuant <= 4)
            {
                player.powerUp();
                chime.Play();
            }
        }

        if (other.gameObject.name == "ClearArea")
        {
            Destroy(gameObject);
        }
    }
}
