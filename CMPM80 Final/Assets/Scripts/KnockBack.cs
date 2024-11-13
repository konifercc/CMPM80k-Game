using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockbackDuration = 0.2f; // Duration to pause enemy movement after knockback

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        rb.linearVelocity = Vector2.zero; // Stop any current movement
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        // Pause movement
        EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
        if (enemyFollow != null)
        {
            enemyFollow.DisableMovement(knockbackDuration);
        }
    }
}