using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyFollow enemyFollow;
    public float knockbackDuration = 0.3f;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on enemy. Please attach a Rigidbody2D component.");
        }
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb != null)
        {
            rb.AddForce(direction * force, ForceMode2D.Impulse);

            // Disable movement temporarily if EnemyFollow is attached
            EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollow.DisableMovement(knockbackDuration);
            }

            Invoke("StopKnockback", knockbackDuration);
        }
    }

    private void StopKnockback()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop movement after knockback duration
        } 
    }
}