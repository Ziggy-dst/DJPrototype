using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShotGunProjectileDestroyingPlayerBullet : MonoBehaviour
{
    private List<GameObject> playerBullets;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet").ToList();
        Vector2 shotGunBulletPos = new Vector2(transform.position.x, transform.position.z);
        foreach (var bullet in playerBullets)
        {
            Vector2 bulletPos = new Vector2(bullet.transform.position.x, bullet.transform.position.z);
            if (Vector2.Distance(bulletPos, shotGunBulletPos) <= 0.5f)
            {
                bullet.SetActive(false);
                // gameObject.SetActive(false);
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("PlayerBullet"))
    //     {
    //         print("collision!");
    //         Destroy(other);
    //         Destroy(gameObject);
    //     }
    // }
}
