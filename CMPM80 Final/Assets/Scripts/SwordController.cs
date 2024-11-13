using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator animator;
    private Collider2D swordCollider;
    public float slashCooldown = 0.5f; // Cooldown time in seconds
    private float cooldownTimer = 0.0f;
    public float colliderActiveTime = 0.2f; // Time the collider stays active during the slash

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if Animator is correctly assigned
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject. Please ensure it's attached.");
        }

        // Get the Collider2D component attached to this GameObject
        swordCollider = GetComponent<Collider2D>();

        // Check if Collider is correctly assigned
        if (swordCollider == null)
        {
            Debug.LogError("Collider2D component not found on this GameObject. Please ensure it's attached.");
        }
        else
        {
            swordCollider.enabled = false; // Initially disable the collider
        }
    }

    void Update()
    {
        // Decrease the cooldown timer over time
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Check if "Z" is pressed and if the cooldown is complete
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z key pressed"); // Debug log to confirm key press

            if (cooldownTimer <= 0)
            {
                animator.SetTrigger("BladeUpwardSlash"); // Trigger the slash animation
                Debug.Log("BladeUpwardSlash animation triggered"); // Debug log to confirm trigger
                cooldownTimer = slashCooldown; // Reset the cooldown timer

                // Enable the sword collider and disable it after a short delay
                swordCollider.enabled = true;
                Invoke("DisableCollider", colliderActiveTime); // Disable collider after colliderActiveTime
            }
            else
            {
                Debug.Log("Slash on cooldown"); // If cooldown is active
            }
        }
    }

    // Method to disable the sword collider
    private void DisableCollider()
    {
        swordCollider.enabled = false;
    }
}