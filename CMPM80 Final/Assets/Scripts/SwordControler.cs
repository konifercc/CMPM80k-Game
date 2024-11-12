using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator animator;
    private bool isSlashing = false;

    // Cooldown variables
    public float slashCooldown = 1.0f; // 1-second cooldown
    private float cooldownTimer = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update the cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Trigger slash only if the cooldown has finished
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0)
        {
            animator.SetTrigger("Slash");
            isSlashing = true;
            cooldownTimer = slashCooldown; // Reset the cooldown timer
        }

        // Reset the isSlashing flag when the animation finishes
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isSlashing = false;
        }
    }
}