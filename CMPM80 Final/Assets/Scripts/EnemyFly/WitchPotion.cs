using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WitchPotion : MonoBehaviour
{
    [SerializeField] private float Damage = 50f;
    [SerializeField] private float lifetime = 1f;
    [SerializeField] public float speed = 7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
            return;
        }
        rb.gravityScale = 0;

        Debug.Log("Destroying game object in " + lifetime + " seconds.");
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerhealth = GameObject.FindWithTag("PlayerTag").GetComponent<PlayerHealth>();

        if (collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("Hit something?");
            playerhealth.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    /*
    private void HandleCollision(GameObject target)
    {
        // Check if the target has an EnemyHealth component and deal damage
        EnemyHealth enemy = target.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            enemy.TakeDamageE(damage); // Deal damage to the enemy
        }

        // Check if the target has a MinotaurHealth component and deal damage
        MinotaurHealth boss = target.GetComponent<MinotaurHealth>();
        if (boss != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity); // Spawn particle effect
            boss.TakeDamage(damage); // Deal damage to the boss
        }

        // Destroy the fireball after hitting something
        Destroy(gameObject);
    }
    */
}
