using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D EnemyRB;
    public EnemyData Data;
    public float timer;
    private float direction = -1; // 1 for right, -1 for left
    private bool FacingRight;
    public float speed;

    [SerializeField] private Transform hitBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        EnemyRB = GameObject.FindWithTag("EnemyTag").GetComponent<Rigidbody2D>();
        if (EnemyRB != null)
        {
            Debug.Log("Asset Found");
        }
    }
    void Start()
    {
        SetGravScale(Data.gravityscale);
        FacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Data.directionChangeInt)
        {
            timer = 0f;
            direction *= -1; // Reverse direction
            flipSprite();
        }
    }
    private void FixedUpdate()
    {
        float targetSpeed = direction * Data.maxSpeedE;

        // Apply acceleration or deceleration
        if (Mathf.Abs(speed) < Mathf.Abs(targetSpeed))
        {
            speed += direction * Data.runAccelE * Time.fixedDeltaTime;
        }
        else
        {
            speed -= direction * Data.runDecelE * Time.fixedDeltaTime;
        }

        // Clamp speed to max speed
        speed = Mathf.Clamp(speed, -Data.maxSpeedE, Data.maxSpeedE);

        // Move the enemy
        EnemyRB.linearVelocity = new Vector2(speed, EnemyRB.linearVelocity.y);
    }
    public void SetGravScale(float scale)
    {
        EnemyRB.gravityScale = scale;
    }


    private void flipSprite()
    {
        FacingRight = !FacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invert the x scale to flip
        transform.localScale = localScale;
    }
}
