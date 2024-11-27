using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth { get; private set; }
    public float maxHealth { get; private set; } = 100.0f;
    public float minHealth { get; private set; } = 0.0f;
    //checks for receiveDamage call
    [HideInInspector] public bool isAttacked;
    [HideInInspector] public bool recieveFallDamage;
    [HideInInspector] public bool isRespawning;

    public HealthBarScript healthBar;
    public GameController gamecontroller;


    private void Awake()
    {
        Debug.Log("started");
        if(gamecontroller == null)
        {
            gamecontroller = GetComponent<GameController>();
        }
        //healthBar = GameObject.FindWithTag("HealthBarTag").GetComponent<HealthBarScript>();
    }
    void Start()
    {
        isRespawning = false;
        playerHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.setHealth(playerHealth);
        playerHealth = Mathf.Clamp(playerHealth, minHealth, maxHealth);
        if (playerHealth <= 0 && !isRespawning)
        {
            //string currentSceneName = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene(currentSceneName);
            StartCoroutine(gamecontroller.Respawn(0.5f));
        }
    }

    //skeleton for taking damage
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        //Debug.Log(playerHealth);
    }

    public void AddHealth(float healthadd)
    {
        playerHealth += healthadd;  
    }
    public void SetMaxHealth()
    {
        playerHealth = maxHealth;
        Debug.Log("set to max health");
    }
}
