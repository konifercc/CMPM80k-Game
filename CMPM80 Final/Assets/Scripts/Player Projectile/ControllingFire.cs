using UnityEngine;

public class ControllingFire : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab; // Fireball prefab
    [SerializeField] private Transform fireballSpawnPoint; // Spawn point for the fireball
    [SerializeField] private float fireballSpeed = 10f; // Speed of the fireball
    [SerializeField] private float fireballCooldown = 0.5f; // Cooldown between fireballs
    [SerializeField] private float fireballManaCost = 20f; // Mana cost to shoot the fireball

    private float _lastFireballTime; // Tracks the last time a fireball was shot
    private PlayerMana playerMana; // Reference to the PlayerMana script

    private void Awake()
    {
        // Find the PlayerMana script (assuming it’s attached to the same GameObject or Player)
        playerMana = GetComponent<PlayerMana>();

        // Ensure PlayerMana exists
        if (playerMana == null)
        {
            Debug.LogError("PlayerMana script not found on the player. Please attach PlayerMana to the player GameObject.");
        }
    }

    private void Update()
    {
        // Fireball shooting input
        if (Input.GetKeyDown(KeyCode.X)) // Replace 'X' with your preferred key
        {
            TryShootFireball();
        }
    }

    private void TryShootFireball()
    {
        // Check if there's enough mana to cast the fireball
        if (playerMana != null && playerMana.playerMana < fireballManaCost)
        {
            // Do nothing if there's not enough mana
            return;
        }

        // Enforce cooldown
        if (Time.time - _lastFireballTime < fireballCooldown) return;

        // Deduct mana cost
        playerMana.UseMana(fireballManaCost);

        // Update the cooldown timer
        _lastFireballTime = Time.time;

        // Instantiate the fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Set the fireball's direction and velocity
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Determine direction based on player's localScale.x
            Vector2 fireballDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.linearVelocity = fireballDirection * fireballSpeed;

            // Optional: Flip fireball sprite if moving left
            if (transform.localScale.x < 0)
            {
                fireball.transform.localScale = new Vector3(-fireball.transform.localScale.x, fireball.transform.localScale.y, fireball.transform.localScale.z);
            }
        }
    }
}