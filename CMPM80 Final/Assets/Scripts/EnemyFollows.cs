using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Rigidbody2D rb;
    private bool canMove = true; // Flag to control movement

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    // Method to disable movement for a specified time (used for knockback)
    public void DisableMovement(float duration)
    {
        canMove = false;
        Invoke("EnableMovement", duration);
    }

    private void EnableMovement()
    {
        canMove = true;
    }
}