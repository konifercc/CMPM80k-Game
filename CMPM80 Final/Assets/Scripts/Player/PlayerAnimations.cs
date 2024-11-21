using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public PlayerMovement playerMovement;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRunning", playerMovement._moveInput.x != 0);
        //anim.SetBool("isGrounded", playerMovement.isGrounded || playerMovement.IsJumping);
        anim.SetBool("isGrounded", Physics2D.OverlapBox(playerMovement._groundCheckPoint.position, playerMovement._groundCheckSize, 0, playerMovement._groundLayer) && !playerMovement.IsJumping);
        anim.SetBool("isSliding", playerMovement.IsSliding);
        //Debug.Log(playerMovement.IsSliding);
    }
}
 