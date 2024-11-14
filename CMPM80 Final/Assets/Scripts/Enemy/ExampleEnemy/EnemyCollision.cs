using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    PlayerHealth playerhealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerhealth = GameObject.FindWithTag("PlayerTag").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("PlayerTag"))
            {
                Debug.Log("proper collision");
                playerhealth.TakeDamage(20);
                Debug.Log(playerhealth.playerHealth);
            }
            else
            {
                Debug.Log("Playerobject not found");
            }
        }
    }
}
