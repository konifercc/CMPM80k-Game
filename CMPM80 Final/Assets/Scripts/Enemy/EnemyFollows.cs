using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 100f;
    EnemyPatrol enemyPatrol;
    public float distanceToPlayer;

    private Rigidbody2D rb;
    private bool canMove = true; // Flag to control movement
    public bool followMode { get; private set;} = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (canMove && distanceToPlayer < 3.5f)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
        followMode = true;
    }

    // Method to disable movement for a specified time (used for knockback)
    public void DisableMovement(float duration)
    {
        canMove = false;
        enemyPatrol.isKnockedBack = true;
        Invoke("EnableMovement", duration);
    }

    private void EnableMovement()
    {
        canMove = true;
        enemyPatrol.isKnockedBack = false;
    }
}