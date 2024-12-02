using UnityEngine;

public class MinotaurHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;
    public GameObject ground;
    public GameObject portal;

    private void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Portal");
        portal.gameObject.SetActive(false);
        if(portal == null)
        {
            Debug.LogError("Portal object not found.");
        }
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Minotaur took {damage} damage. Current health: {currentHealth}");

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
        Debug.Log("Minotaur defeated!");
        Destroy(ground);
        if (animator != null)
        {
            animator.SetTrigger("Death"); // Trigger death animation
        }
        portal.gameObject.SetActive(true);

        Destroy(gameObject, 1f); // Delay destruction to allow animation to play
    }
}