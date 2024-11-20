using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public EnemyPatrol enemyPatrol;
    private Rigidbody2D rb;

    public float speed = 100f;
    public float distanceToPlayer;
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
        if (followMode && distanceToPlayer >= 3.5f)
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
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
        //invoke flip from enemypatrol when enemy not facing player
        
        if (direction.x < 0 && enemyPatrol.isFacingRight)
        {
            enemyPatrol.Flip();
        }
        else if (direction.x > 0 && !enemyPatrol.isFacingRight)
        {
            enemyPatrol.Flip();
        }
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