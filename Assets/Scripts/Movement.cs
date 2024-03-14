using UnityEngine;

public class Movement : MonoBehaviour
{
    // Speed
    public float moveSpeed = 5f;

    void FixedUpdate()
    {
        // Inputs from keyboard. W and S - Vertical. A and D - horizontal
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate the vector of movement direction
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;

        // Calculate the final position of the player
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Move the player
        transform.position = newPosition;
    }
}
