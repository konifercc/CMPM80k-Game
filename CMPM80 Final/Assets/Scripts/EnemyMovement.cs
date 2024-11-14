using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public EnemyData Data;
    public Transform player;
    public float direction = -1; // 1 for right, -1 for left
    public bool FacingRight;
    public float speed;
    float targetSpeed;
    private float distanceToPlayer;
    public bool defaultMove;
    public bool canMove;

    [SerializeField] private Transform hitBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        defaultMove = true;
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log("found asset");
        }
    }
    void Start()
    {
        FacingRight = false;
        targetSpeed = direction * Data.maxSpeedE;

        //knockback detect player object
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerTag").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetGravScale(Data.gravityscale);
        if (Mathf.Abs(speed) < 0.1f && targetSpeed == 0 && defaultMove)
        {
            Debug.Log("hit 0");
            direction *= -1; // Reverse direction
            flipSprite();
            targetSpeed = direction * Data.maxSpeedE;
        }


        //constantly check distance to player to enable follow
        distanceToPlayer = Vector3.Distance(player.position, rb.position);
        if (distanceToPlayer < 4 && canMove)
        {
            defaultMove = false;
            Debug.Log("follow");
            MoveTowardsPlayer();
        }

        if (rb.linearVelocityX > 0)
        {
            direction = 1;
        }
        else 
        { 
            direction = -1; 
        }


    }
    private void FixedUpdate()
    {
        //float targetSpeed= direction * Data.maxSpeedE; 

        // Apply acceleration or deceleration
        if (defaultMove)
        {
            if (Mathf.Abs(speed) < Mathf.Abs(targetSpeed))
            {
                speed += direction * Data.runAccelE * Time.fixedDeltaTime;
            }
            else if (Mathf.Abs(speed) >= Mathf.Abs(targetSpeed) || targetSpeed == 0)
            {
                targetSpeed = 0f;
                speed -= direction * Data.runDecelE * Time.fixedDeltaTime;
            }

            // Clamp speed to max speed
            speed = Mathf.Clamp(speed, -Data.maxSpeedE, Data.maxSpeedE);

            // Move the enemy
            rb.linearVelocityX = speed;
        }

    }

    //set enemy gravity for RB
    public void SetGravScale(float scale)
    {
        rb.gravityScale = scale;
    }


    //Called wheneber velocity at zero (switch directions)
    private void flipSprite()
    {
        FacingRight = !FacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invert the x scale to flip
        transform.localScale = localScale;
    }


    //flip sprite after knockback
    public void flipSpriteKnockback()
    {
        if (direction == -1 && FacingRight)
        {
            FacingRight = !FacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Invert the x scale to flip
            transform.localScale = localScale;
        }
        else if (direction == 1 && !FacingRight)
        {
            FacingRight = !FacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Invert the x scale to flip
            transform.localScale = localScale;
        }
    }

    //
    // Knockback And Follow Movement
    //

    private void MoveTowardsPlayer()
    {
        Vector2 followDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = -followDirection * speed;
        flipSpriteKnockback();
    }

    // Method to disable movement for a specified time
    public void DisableMovement(float duration)
    {
        canMove = false;
        if (distanceToPlayer <= 4)
        {
            Invoke("EnableMovement", duration);
            Invoke("flipSpriteKnockback", duration + 0.05f);
        }
        else 
        {
            Invoke("EnableDefault", duration);
            Invoke("flipSpriteKnockback", duration + 0.05f);
            Invoke("EnableMovement", duration); //allows for distance trigger to be set
        }
    }


    private void EnableDefault()
    {
        defaultMove = true;
    }
    private void EnableMovement()
    {
        canMove = true;
    }
}
