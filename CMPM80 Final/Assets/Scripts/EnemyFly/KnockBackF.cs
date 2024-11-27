using UnityEngine;

public class KnockBackF : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyFly enemyFly;
    public float knockbackDuration = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb != null)
        {
            Debug.Log("Knockback applied");
            rb.AddForce(direction * (1.1f*force), ForceMode2D.Impulse);
            // Disable movement temporarily if EnemyFly is attached
            EnemyFly enemyFly = GetComponent<EnemyFly>();
            if (enemyFly != null)
            {
                enemyFly.DisableMovement(knockbackDuration);
            }
            //Invoke("StopKnockback", knockbackDuration);
        }
    }

    public void StopKnockBack()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop movement after knockback duration
        }
        enemyFly.FindFurthestPoint();
        enemyFly.isPathingToY = true;   
    }
}
