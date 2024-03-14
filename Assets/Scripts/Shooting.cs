using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab
    public float fireRate = 0.5f; // Firing rate. In seconds
    public float projectileSpeed = 5f; // Projectile speed
    public Transform spawnPoint; // Projectile spawn point

    private float nextFireTime; // Next time to shoot

    void Update()
    {
        // If enough time has passed since the last shot
        if (Time.time > nextFireTime)
        {
            // Shoot
            Shoot();

            // Set the time of the next shot
            nextFireTime = Time.time + fireRate;
        }
    }

    // Shoot method
    void Shoot()
    {
        // Create a projectile at spawnPoint
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Point the projectile down with setted speed
        rb.velocity = Vector2.down * projectileSpeed;
    }
}
