using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;
    public GameObject ground;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Demon took {damage} damage. Current health: {currentHealth}");

        if (animator != null)
        {
            animator.SetTrigger("Damage"); // Play damage reaction animation
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Demon defeated!");
        Destroy(ground);
        if (animator != null)
        {
            animator.SetTrigger("Death"); // Trigger death animation
        }

        Destroy(gameObject, 1f); // Delay destruction to allow animation to play
    }
}