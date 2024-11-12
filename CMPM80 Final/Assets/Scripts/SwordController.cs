using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator animator;
    public float slashCooldown = 0.5f; // Cooldown time in seconds
    private float cooldownTimer = 0.0f;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if Animator is correctly assigned
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject. Please ensure it's attached.");
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
            }
            else
            {
                Debug.Log("Slash on cooldown"); // If cooldown is active
            }
        }
    }
}