using UnityEngine;

public class DemonAI : MonoBehaviour
{
    public float detectionRange = 5f;  // Range to detect the player
    public float attackRange = 1f;    // Range for attack
    public float moveSpeed = 2f;      // Speed of the demon

    private Transform player;         // Reference to the player's Transform
    private Rigidbody2D rb;           // Reference to the Rigidbody2D component
    private Animator animator;        // Reference to the Animator component
    private DemonAttack demonAttack; // Reference to DemonAttack

    private void Start()
    {
        // Find the player object
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("[ERROR] Player object not found in the scene.");
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        demonAttack = GetComponent<DemonAttack>();
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Face the player
        FacePlayer();

        // Handle movement and attack logic
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopMoving();
                demonAttack.StartAttack(); // Trigger attack when in range
            }
        }
        else
        {
            StopMoving(); // Idle if player is out of detection range
        }
    }

    private void MoveTowardsPlayer()
    {
        animator.SetBool("isRunning", true);

        // Move the Minotaur towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
    }

    private void StopMoving()
    {
        animator.SetBool("isRunning", false);
        rb.linearVelocity = Vector2.zero;
    }

    private void FacePlayer()
    {
        float directionX = player.position.x - transform.position.x;

        if (directionX > 0 && transform.localScale.x < 0)
        {
            // Face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0 && transform.localScale.x > 0)
        {
            // Face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}