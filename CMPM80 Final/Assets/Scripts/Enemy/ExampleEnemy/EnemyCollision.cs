using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    PlayerHealth playerhealth;
    public float damageTime;
    public float colliderCooldown;


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
            colliderCooldown = 1f; //collider cooldown for playerhitbox
            Debug.Log(playerhealth.playerHealth);
            Debug.Log("DamageTime" + damageTime + "Time.time" + Time.time);
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
