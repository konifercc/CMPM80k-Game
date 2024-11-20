using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    PlayerHealth playerhealth;
    public float damageTime; //last time player took damage
    public float colliderCooldown; //cooldown for continous collision damage


    void Awake()
    {
        damageTime = 0f; //sets last time damaged to 0, instant first hit
        playerhealth = GameObject.FindWithTag("PlayerTag").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerhealth == null)
        {
            playerhealth = GameObject.FindWithTag("PlayerTag").GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("collision detected");
            colliderCooldown = 1f; 
            if (Time.time - damageTime >= colliderCooldown)
            {
                playerhealth.TakeDamage(20);
                damageTime = Time.time;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            damageTime = 0f;      
        }
    }
}
