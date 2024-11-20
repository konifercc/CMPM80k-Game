using UnityEngine;

public class ControllingFire : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab; // Fireball prefab
    [SerializeField] private Transform fireballSpawnPoint; // Spawn point for the fireball
    [SerializeField] private float fireballSpeed = 10f; // Speed of the fireball
    [SerializeField] private float fireballCooldown = 0.5f; // Cooldown between fireballs

    private float _lastFireballTime; // Tracks the last time a fireball was shot
    private PlayerMovement _playerMovement; // Reference to the PlayerMovement script

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement script
    }

    private void Update()
    {
        // Fireball shooting input
        if (Input.GetKeyDown(KeyCode.X)) // Replace 'X' with your preferred key
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        // Enforce cooldown
        if (Time.time - _lastFireballTime < fireballCooldown) return;

        // Update the cooldown timer
        _lastFireballTime = Time.time;

        // Instantiate the fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Set the fireball's direction and velocity
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Determine direction based on player's facing direction
            Vector2 fireballDirection = _playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
            rb.linearVelocity = fireballDirection * fireballSpeed;

            // Optional: Flip fireball sprite if moving left
            if (!_playerMovement.IsFacingRight)
            {
                fireball.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}