using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // Default damage value
    [SerializeField] private float lifetime = 3f; // Lifetime of the fireball in seconds
    [SerializeField] private float manaCost; // Mana cost to cast the fireball
    public GameObject particle;

    private PlayerMana playerMana; // Reference to the PlayerMana script

    private void Start()
    {
        // Find the PlayerMana script (assuming it's attached to the same GameObject or Player)
        playerMana = Object.FindFirstObjectByType<PlayerMana>();

        // Destroy the fireball after its lifetime
        Destroy(gameObject, lifetime);
    }

    public void TryShootFireball(GameObject fireballPrefab, Transform firePoint)
    {
        // Check if the player has enough mana
        if (playerMana != null && playerMana.playerMana >= manaCost)
        {
            // Subtract mana and shoot the fireball
            playerMana.UseMana(manaCost);

            // Instantiate the fireball
            Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.Log("Not enough mana to cast fireball!");
        }
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