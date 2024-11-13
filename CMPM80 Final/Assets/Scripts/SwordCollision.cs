using UnityEngine;

public class Sword : MonoBehaviour
{
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Knockback enemyKnockback = collision.GetComponent<Knockback>();
            if (enemyKnockback != null)
            {
                enemyKnockback.ApplyKnockback(knockbackDirection, knockbackForce);
            }
        }
    }
}