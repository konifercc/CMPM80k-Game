using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;
    PlayerHealth playerhealth;
    PlayerMovement playerMovement;
    float xScale;

    private void Start(){
        checkPointPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerhealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Spike")){
            StartCoroutine(Respawn(0.5f));
        }
    }


    public void updateCheckpoint(Vector2 pos){
        checkPointPos = pos;
    }

    public IEnumerator Respawn(float respawnCooldown){

        
        
        playerhealth.isRespawning = true;
        playerhealth.SetMaxHealth();
        rb.linearVelocity = new Vector2(0,0);
        if (transform.localScale.x < 0){
            playerMovement.Turn();
        }
        rb.simulated = false;
        transform.localScale = new Vector3(0,0,1);
        yield return new WaitForSeconds(respawnCooldown);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1,1,1);
        rb.simulated = true;
        playerhealth.isRespawning = false;
        
    }

}
