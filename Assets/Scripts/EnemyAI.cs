using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string playerTag = "Player";
    public float moveSpeed = 5f;
    public float health = 100f;

    private Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Получаем скрипт Rifle у объекта, на котором висит пуля
            Rifle rifleScript = collision.gameObject.GetComponent<Rifle>();

            // Проверяем, что скрипт найден
            if (rifleScript != null)
            {
                // Отнимаем здоровье объекта EnemyAI на количество, указанное в переменной damage скрипта Rifle
                health -= rifleScript.damage;

                // После попадания пули врага, уничтожаем пулю
                Destroy(collision.gameObject);

                // Проверяем, остался ли у объекта здоровье
                if (health <= 0)
                {
                    // Если здоровье меньше или равно 0, уничтожаем объект EnemyAI
                    Destroy(gameObject);
                }
            }
        }
    }
}
