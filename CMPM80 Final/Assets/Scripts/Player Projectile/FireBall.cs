using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // Default damage value
    [SerializeField] private float lifetime = 3f; // Lifetime of the fireball in seconds
    public GameObject particle;

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
        // Check if the object hit is an enemy
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        // Check if the object hit is a boss (e.g., Minotaur)
        MinotaurHealth boss = collision.gameObject.GetComponent<MinotaurHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage); // Deal damage to the boss
        }

        Destroy(gameObject); // Destroy the fireball after collision
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the object hit is an enemy
        EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
        
        if (enemy != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        // Check if the object hit is a boss (e.g., Minotaur)
        MinotaurHealth boss = collider.gameObject.GetComponent<MinotaurHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage); // Deal damage to the boss
        }

        Destroy(gameObject); // Destroy the fireball after collision
    }
}