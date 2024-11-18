using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // Default damage value
    [SerializeField] private float lifetime = 3f; // Lifetime of the fireball in seconds

    private void Start()
    {
        // Schedule the fireball to be destroyed after its lifetime
        Destroy(gameObject, lifetime);
    }

    // Public method to set the fireball's damage
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        Destroy(gameObject); // Destroy the fireball after collision
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        Destroy(gameObject); // Destroy the fireball after collision
    }
}