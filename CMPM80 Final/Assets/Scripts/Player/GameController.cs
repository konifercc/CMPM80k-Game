using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;
    PlayerHealth playerhealth;


    private void Awake()
    {
        playerhealth = GetComponent<PlayerHealth>();
    }
    private void Start(){
        checkPointPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Spike")){
            StartCoroutine(Respawn(0.5f));
        }

        //player lose health to enemy
        if (collision.CompareTag("EnemyTag")){
            Debug.Log("contact");
            if (collision != null)
            {
                if (playerhealth != null)
                {
                    // Do something with playerHealth
                    playerhealth.recieveDamage(20);

                }
                else
                {
                    Debug.LogError("PlayerHealth component is missing on the collided object.");
                }
            }
            else
            {
                Debug.LogError("Collider is null.");
            }
        }
    }


    public void updateCheckpoint(Vector2 pos){
        checkPointPos = pos;
    }

    IEnumerator Respawn(float respawnCooldown){
        
        rb.linearVelocity = new Vector2(0,0);
        
        rb.simulated = false;
        transform.localScale = new Vector3(0,0,1);
        yield return new WaitForSeconds(respawnCooldown);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1,1,1);
        rb.simulated = true;
        
    }

}
