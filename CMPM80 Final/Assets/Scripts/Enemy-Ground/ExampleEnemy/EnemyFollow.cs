using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public EnemyPatrol enemyPatrol;
    private Rigidbody2D rb;
    private GameObject[] spikes;

    public float speed = 10f;
    public float distanceToPlayer;
    public float followDistance = 5f;
    private float distanceToSpike;
    private bool canMove = true; // Flag to control movement
    public bool isJumping;
    public bool followMode { get; private set;} = false; 
    

    private void Start()
    {
        isJumping = false;
        spikes = GameObject.FindGameObjectsWithTag("SpikeHitBox");
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (canMove && distanceToPlayer < followDistance)
        {
            MoveTowardsPlayer();
        }
        if (followMode && distanceToPlayer >= followDistance)
        {
            followMode = false;

            if (Vector2.Distance(transform.position, enemyPatrol.pointA.transform.position) > Vector2.Distance(transform.position, enemyPatrol.pointB.transform.position))
            {
                enemyPatrol.setCurrentPoint(enemyPatrol.pointA);
                if (enemyPatrol.isFacingRight)
                {
                    enemyPatrol.Flip();
                }
            }
            else
            {
                enemyPatrol.setCurrentPoint(enemyPatrol.pointB);
                if (!enemyPatrol.isFacingRight)
                {
                    enemyPatrol.Flip();
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        //Vector2 targetPosition = new Vector2(player.position.x, rb.position.y); //lock Y movement when folowing
        rb.linearVelocityX = direction * speed;

        // Check if enemy is walking towards a spike        
        float nearestSpike = GetDistanceToNearestSpike();
        if (nearestSpike < 1f && !isJumping)
        {
            JumpSpike();
            isJumping = false;
        }

        // Invoke flip from EnemyPatrol when enemy not facing player
        if (direction < 0 && enemyPatrol.isFacingRight)
        {
            enemyPatrol.Flip();
        }
        else if (direction > 0 && !enemyPatrol.isFacingRight)
        {
            enemyPatrol.Flip();
        }
        followMode = true;
    }

    // Method to disable movement for a specified time (used for knockback)
    public void DisableMovement(float duration)
    {
        canMove = false;
        enemyPatrol.isKnockedBack = true;
        Invoke("EnableMovement", duration);
    }

    // Method to enable movement after a specified time
    public void EnableMovement()
    {
        canMove = true;
        enemyPatrol.isKnockedBack = false;
    }


    public void JumpSpike()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
    }

    float GetDistanceToNearestSpike()
    {
        float nearestDistance = Mathf.Infinity;
        GameObject nearestSpike = null;

        // Loop through each spike and calculate the distance
        foreach (GameObject spike in spikes)
        {
            float distance = Vector2.Distance(transform.position, spike.transform.position);

            // Check if this spike is the closest one
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestSpike = spike;
            }
        }
        return nearestDistance;
    }


}
