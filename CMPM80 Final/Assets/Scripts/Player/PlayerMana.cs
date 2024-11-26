using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    // Mana properties
    public float playerMana { get; private set; }
    public float maxMana { get; private set; } = 100.0f;
    public float minMana { get; private set; } = 0.0f;

    public ManaBarScript manaBar; // Reference to ManaBar UI script
    public float manaRegenRate = 5.0f; // Mana regenerated per second
    public float manaCost; // Mana cost per spell cast
    public float regenDelay = 2.0f; // Time before regeneration starts

    private bool isRegenerating = false; // Tracks if mana is regenerating
    private float lastManaUseTime = 0f; // Tracks the time of the last mana use

    private void Awake()
    {
        manaBar = GameObject.FindWithTag("ManaBarTag").GetComponent<ManaBarScript>();
    }

    private void Start()
    {
        playerMana = maxMana;
        manaBar.setMaxMana(maxMana); // Initialize UI to max mana
    }

    private void Update()
    {
        // Update the mana bar UI
        manaBar.setMana(playerMana);

        // Clamp mana between min and max values
        playerMana = Mathf.Clamp(playerMana, minMana, maxMana);

        // Detect "X" key press to cast a spell
        if (Input.GetKeyDown(KeyCode.X))
        {
            UseMana(manaCost);
        }

        // Trigger regeneration if mana is not full and delay has passed
        if (!isRegenerating && Time.time - lastManaUseTime >= regenDelay && playerMana < maxMana)
        {
            StartCoroutine(RegenerateMana());
        }
    }

    public void UseMana(float manaCost)
    {
        // Check if enough mana is available
        if (playerMana >= manaCost)
        {
            playerMana -= manaCost; // Reduce mana
            lastManaUseTime = Time.time; // Record the time of the mana use
            isRegenerating = false; // Stop current regeneration
            StopAllCoroutines(); // Cancel any active regeneration coroutines
        }
        else
        {
            Debug.Log("Not enough mana!");
        }
    }

    public void AddMana(float manaAmount)
    {
        playerMana += manaAmount;
    }

    public void SetMaxMana()
    {
        playerMana = maxMana;
        Debug.Log("Mana set to max");
    }

    private System.Collections.IEnumerator RegenerateMana()
    {
        isRegenerating = true;

        // Regenerate mana over time
        while (playerMana < maxMana)
        {
            playerMana += manaRegenRate * Time.deltaTime; // Increment mana
            playerMana = Mathf.Min(playerMana, maxMana); // Clamp to max mana
            manaBar.setMana(playerMana); // Update UI
            yield return null; // Wait for next frame
        }

        isRegenerating = false; // Stop regenerating when full
    }
}