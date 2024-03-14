using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnRate = 5f;
    public int objectsToSpawnPerWave = 5;
    public float spawnRadius = 5f;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWave();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < objectsToSpawnPerWave; i++)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector2 spawnPosition = GetValidSpawnPosition();
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPosition;
        do
        {
            spawnPosition = new Vector2(Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius),
                                        Random.Range(transform.position.y - spawnRadius, transform.position.y + spawnRadius));
        } while (Vector2.Distance(spawnPosition, (Vector2)transform.position) < spawnRadius);

        return spawnPosition;
    }
}
