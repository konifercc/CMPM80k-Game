using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float startTimeBtwAttack = 0.3f;          // Cooldown time between attacks
    public Transform attackPos;                      // Position in front of the player for the attack
    public LayerMask whatIsEnemy;                    // Layer mask to define what is considered an enemy
    public Vector2 attackBoxSize = new Vector2(2.0f, 1.0f); // Size of the rectangular attack area
    public float knockbackForce = 15f;               // Knockback force applied to the enemy or boss
    public float attackDamage = 20f;                 // Default damage value (modifiable)

    private Animator animator;
    private float timeBtwAttack;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on Player object.");
        }
    }

    void Update()
    {
        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }

        // Trigger attack when pressing "Z" and cooldown allows
        if (Input.GetKeyDown(KeyCode.Z) && timeBtwAttack <= 0)
        {
            PerformSlash();
            timeBtwAttack = startTimeBtwAttack; // Reset cooldown timer
        }
    }

    void PerformSlash()
    {
        // Play the attack animation
        if (animator != null)
        {
            animator.SetTrigger("BladeUpwardSlash"); // Trigger the attack animation
            Debug.Log("Attack animation triggered.");
        }

        // Detect enemies and bosses within the rectangular attack area
        Collider2D[] targetsToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackBoxSize, 0, whatIsEnemy);

        foreach (var targetCollider in targetsToDamage)
        {
            Debug.Log("Entered Loop");

            // Apply knockback if the target has a KnockBack component
            KnockBack knockbackComponent = targetCollider.GetComponent<KnockBack>();
            if (knockbackComponent != null)
            {
                // Calculate knockback direction (from player to target)
                Vector2 knockbackDirection = (targetCollider.transform.position - transform.position).normalized;
                // Apply knockback with direction and force
                knockbackComponent.ApplyKnockback(knockbackDirection, knockbackForce);
            }

            // Apply damage to enemies
            EnemyHealth enemy = targetCollider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamageE(attackDamage); // Use dynamic damage value
                continue; // Skip further checks if it's an enemy
            }

            // Apply damage to bosses
            MinotaurBehavior boss = targetCollider.GetComponent<MinotaurBehavior>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage); // Use dynamic damage value
            }
        }
    }

    // Public method to adjust the damage dynamically
    public void SetAttackDamage(float newDamage)
    {
        attackDamage = newDamage;
        Debug.Log("Attack damage updated to: " + attackDamage);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red rectangle to visualize the attack area in the Scene view
        if (attackPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(attackPos.position, Quaternion.identity, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, attackBoxSize);
        }
    }
}