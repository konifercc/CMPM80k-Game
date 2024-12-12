using System.Collections;
using UnityEngine;

public class MinotaurBehavior : MonoBehaviour
{
    public float detectionRange = 5f; // Range to detect the player
    public float attackRange = 1f; // Range for attack
    public float moveSpeed = 2f; // Speed of the Minotaur
    public float attackCooldown = 2f; // Time between attacks
    public float swingDelay = 0.5f; // Time to pause before swinging
    public int damage = 10; // Damage dealt to the player
    public float maxHealth = 100f; // Maximum health

    private float currentHealth; // Current health of the Minotaur
    private Transform player; // Reference to the player's Transform
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isAttacking = false; // Tracks if the Minotaur is currently attacking

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initialize health
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        // Always ensure the Minotaur is facing the player
        FacePlayer();

        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            if (distanceToPlayer > attackRange)
            {
                // Move towards the player if not within attack range
                MoveTowardsPlayer();
            }
            else
            {
                // Stop running and prepare to attack
                StopRunning();
                StartCoroutine(PrepareAndAttack());
            }
        }
        else
        {
            // Idle state if the player is out of range
            animator.SetBool("isRunning", false);
            rb.linearVelocity = Vector2.zero; // Stop movement when idle
        }
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("isRunning", true);

        // Calculate the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the Minotaur towards the player
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
    }

    void StopRunning()
    {
        animator.SetBool("isRunning", false);
        rb.linearVelocity = Vector2.zero;
    }

    void FacePlayer()
    {
        // Calculate direction to the player
        float directionX = player.position.x - transform.position.x;

        if (directionX > 0 && transform.localScale.x < 0)
        {
            // Face right while keeping the scale magnitude
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0 && transform.localScale.x > 0)
        {
            // Face left while keeping the scale magnitude
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator PrepareAndAttack()
    {
        isAttacking = true;

        // Wait for the swing delay before attacking
        yield return new WaitForSeconds(swingDelay);

        // Play attack animation
        animator.SetTrigger("Attack");

        // Deal damage to the player
        if (player.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        // Wait for the attack cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount

        // Check if health is zero or below
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the Minotaur object
        }
    }
}