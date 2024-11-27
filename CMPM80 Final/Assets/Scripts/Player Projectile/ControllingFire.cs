using UnityEngine;

public class ControllingFire : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab; // Fireball prefab
    [SerializeField] private Transform fireballSpawnPoint; // Spawn point for the fireball
    [SerializeField] private float fireballSpeed = 10f; // Speed of the fireball
    [SerializeField] private float fireballCooldown = 0.5f; // Cooldown between fireballs
    [SerializeField] private float fireballMinMana = 50f; // Minimum mana required to shoot fireball

    private float _lastFireballTime; // Tracks the last time a fireball was shot
    private PlayerMana playerMana; // Reference to the PlayerMana script

    private void Awake()
    {
        // Attempt to get the PlayerMana script from the same GameObject
        playerMana = GetComponent<PlayerMana>();

        if (playerMana == null)
        {
            Debug.LogError("PlayerMana script not found on the player GameObject. Please attach it.");
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
        if (fireballPrefab == null)
        {
            Debug.LogError("FireballPrefab is not assigned in the Inspector!");
            return;
        }

        if (fireballSpawnPoint == null)
        {
            Debug.LogError("FireballSpawnPoint is not assigned in the Inspector!");
            return;
        }

        // Check if there is enough mana
        if (!playerMana.UseMana(fireballMinMana))
        {
            Debug.Log("Not enough mana to shoot fireball! Minimum 50 mana required.");
            return;
        }

        // Check if cooldown has passed
        if (Time.time - _lastFireballTime < fireballCooldown)
        {
            Debug.Log("Fireball is on cooldown!");
            return;
        }

        _lastFireballTime = Time.time;

        // Instantiate and launch the fireball
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Fireball prefab is missing a Rigidbody2D component!");
            return;
        }

        Vector2 fireballDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.linearVelocity = fireballDirection * fireballSpeed;

        // Flip the fireball if the player is facing left
        if (transform.localScale.x < 0)
        {
            fireball.transform.localScale = new Vector3(-fireball.transform.localScale.x, fireball.transform.localScale.y, fireball.transform.localScale.z);
        }
    }
}