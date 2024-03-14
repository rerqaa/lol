using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Projectile prefab
    private GameObject[] enemies; // Array to store all enemies

    public float fireRate = 0.5f; // Firing rate. In seconds
    public float projectileSpeed = 5f; // Projectile speed
    public float spreadAngle = 45f; // Spread angle for shotgun
    public int numberOfPellets = 5; // Number of pellets per shot
    private Transform spawnPoint; // Projectile spawn point (Now it's a private variable)
    private float nextFireTime; // Next time to shoot

    void Start()
    {
        // Get the spawnPoint from the current GameObject
        spawnPoint = transform;
        // Set bullet object
        projectilePrefab = GameObject.Find("Bullet_Shotgun");
    }

    void Update()
    {
        // If enough time has passed since the last shot
        if (Time.time > nextFireTime)
        {
            // Find all enemies with tag "Enemy"
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Find the closest enemy
            GameObject closestEnemy = GetClosestEnemy(enemies);

            if (closestEnemy != null)
            {
                // Calculate direction towards the closest enemy
                Vector2 directionToEnemy = (closestEnemy.transform.position - spawnPoint.position).normalized;

                // Shoot towards the closest enemy with spread angle
                Shoot(directionToEnemy);
            }

            // Set the time of the next shot
            nextFireTime = Time.time + fireRate;
        }
    }

    // Method to find the closest enemy
    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    // Shoot method
    void Shoot(Vector2 directionToEnemy)
    {
        // Calculate angle increment between each pellet
        float angleIncrement = spreadAngle / (numberOfPellets - 1);

        // Calculate start angle for spread
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < numberOfPellets; i++)
        {
            // Calculate current angle for pellet
            float currentAngle = startAngle + angleIncrement * i;

            // Rotate direction towards enemy by current angle
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * directionToEnemy;

            // Create a projectile at spawnPoint
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            // Get the Rigidbody2D component of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Shoot the projectile with the calculated direction and speed
            rb.velocity = direction * projectileSpeed;

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5f);
        }
    }
}
