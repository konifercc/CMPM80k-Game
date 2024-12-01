using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private float damage = 10f; // Damage dealt by the fireball
    [SerializeField] private float lifetime = 3f; // Lifetime of the fireball
    [SerializeField] private GameObject particle; // Particle effect prefab

    private void Start()
    {
        // Destroy the fireball after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollision(collider.gameObject);
    }

    private void HandleCollision(GameObject target)
    {
        // Check if the target has an EnemyHealth component and deal damage
        EnemyHealth enemy = target.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        EnemyFHealth enemyF = target.GetComponent<EnemyFHealth>();
        if (enemyF != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            enemyF.TakeDamageF(damage); // Deal damage to the enemy
        }

        // Check if the target has a MinotaurHealth component and deal damage
        MinotaurHealth boss = target.GetComponent<MinotaurHealth>();
        if (boss != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            boss.TakeDamage(damage); // Deal damage to the boss
        }

        // Check if the target has a DemonHealth component and deal damage
        DemonHealth demon = target.GetComponent<DemonHealth>();
        if (demon != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            demon.TakeDamage(damage); // Deal damage to the demon
        }

        // Destroy the fireball after hitting something
        Destroy(gameObject);
    }
}