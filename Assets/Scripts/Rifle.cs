using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] public GameObject projectilePrefab; // Projectile prefab

    public float fireRate = 0.5f; // Firing rate. In seconds
    public float damage = 40f;
    public float projectileSpeed = 5f; // Projectile speed
    private Transform spawnPoint; // Projectile spawn point (Now it's a private variable)

    private float nextFireTime; // Next time to shoot

    void Start()
    {
        // Get the spawnPoint from the current GameObject
        spawnPoint = transform;
        // Set bullet object
        projectilePrefab = GameObject.Find("Bullet_Rifle");
    }

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
        // Find all objects with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            // Find the nearest enemy
            GameObject nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestEnemy = enemy;
                    nearestDistance = distance;
                }
            }

            // If a nearest enemy is found, shoot towards it
            if (nearestEnemy != null)
            {
                // Calculate the direction towards the nearest enemy
                Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;

                // Create a projectile at spawnPoint
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

                // Get the Rigidbody2D component of the projectile
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                // Set the velocity towards the nearest enemy
                rb.velocity = direction * projectileSpeed;

                // Destroy the projectile after 5 seconds
                Destroy(projectile, 5f);
            }
        }
        else
        {
            // If no enemies are found, shoot straight down
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * projectileSpeed;
            Destroy(projectile, 5f);
        }
    }
}
