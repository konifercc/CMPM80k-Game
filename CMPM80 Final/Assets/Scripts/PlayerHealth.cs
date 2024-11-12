using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth {get; private set;}
    public float maxHealth = 100.0f;
    public float minHealth = 0.0f;
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
        //checks for ie if player in contact with enemy etc
        if (Input.GetKeyDown(KeyCode.E) && playerHealth > minHealth)
            {
                recieveDamage(10.0f);
                Debug.Log(playerHealth.ToString());
            }
        healthBar.setHealth(playerHealth);
    }

    //skeleton for taking damage
    public void recieveDamage(float damage)
    {
        playerHealth -= damage;
        healthBar.setHealth(playerHealth);
    }
}
