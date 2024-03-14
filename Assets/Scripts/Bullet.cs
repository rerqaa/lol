using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 5f;

    void Start()
    {
        Invoke("DestroyObject", destroyDelay);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
