using UnityEngine;

public class MinotaurAttack : MonoBehaviour
{
    public float damageAmount = 20f;       // Damage dealt to the player
    public float attackCooldown = 1.5f;   // Cooldown between attacks
    public float colliderCooldown = 1f;   // Cooldown for continuous collision damage
    public Collider2D attackCollider;     // Collider for the attack hitbox

    private PlayerHealth playerHealth;    // Reference to PlayerHealth
    private float lastAttackTime;         // Last time an attack was triggered
    private float lastDamageTime;         // Last time damage was applied
    private Animator animator;            // Reference to Animator component

    private void Awake()
    {
        lastAttackTime = -attackCooldown; // Allow immediate attack at start
        lastDamageTime = 0f;

        // Find the player and their PlayerHealth component
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("[ERROR] Player object not found in the scene.");
        }

        animator = GetComponent<Animator>();


        
    }

    public void StartAttack()
    {
        // Trigger attack animation if cooldown allows
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            lastAttackTime = Time.time; // Reset attack cooldown timer
        }
    }

    // Called via Animation Event to enable the attack collider
    public void EnableAttackCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
            Debug.Log("[DEBUG] Attack collider enabled.");
        }
    }

    // Called via Animation Event to disable the attack collider
    public void DisableAttackCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
            Debug.Log("[DEBUG] Attack collider disabled.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackCollider != null && attackCollider.enabled && collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("[DEBUG] Collision detected with PlayerHitBox.");

            if (Time.time - lastDamageTime >= colliderCooldown)
            {
                PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Debug.Log("[DEBUG] Calling TakeDamage on PlayerHealth.");
                    playerHealth.TakeDamage(damageAmount);
                    lastDamageTime = Time.time;
                }
                else
                {
                    Debug.LogWarning("[WARNING] PlayerHealth component not found on parent.");
                }
            }
        }
    }
}