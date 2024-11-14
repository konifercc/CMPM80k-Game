using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth { get; private set; }
    public float maxHealth { get; private set; } = 100.0f;
    public float minHealth { get; private set; } = 0.0f;
    //checks for receiveDamage call
    [HideInInspector] public bool isAttacked;
    [HideInInspector] public bool recieveFallDamage;

    public HealthBarScript healthBar;

    void Start()
    {
        playerHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.setHealth(playerHealth);
        playerHealth = Mathf.Clamp(playerHealth, minHealth, maxHealth);
    }

    //skeleton for taking damage
    public void recieveDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);
    }
}
