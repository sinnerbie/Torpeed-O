using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject projectile;

    PlayerBehaviour player;

    float fireCount = 0;
    bool fire = false;

    void Start()
    {
        player = FindFirstObjectByType<PlayerBehaviour>();
    }

    void Update()
    {
        transform.LookAt(player.transform);

        if (player.transform.position.z >= transform.position.z - 1 && player.transform.position.z <= transform.position.z + 1)
        {
            if (transform.position.x <= 30 && transform.position.x > 1)
            {
                fire = true;
            }else
            {
                fire = false;
            }
        }else
        {
            fire = false;
        }

        if (fire) fireCount -= Time.deltaTime;

        if (fire && fireCount <= 0) LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Instantiate(projectile, transform.position, transform.rotation);
        fireCount = 1.5f;
    }
}
