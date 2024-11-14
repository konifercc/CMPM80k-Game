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
    }


    public void updateCheckpoint(Vector2 pos){
        checkPointPos = pos;
    }

    IEnumerator Respawn(float respawnCooldown){
        
        transform.localScale = new Vector3(0,0,1);
        rb.simulated = false;
        rb.linearVelocity = new Vector2(0,0);
        yield return new WaitForSeconds(respawnCooldown);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1,1,1);
        rb.simulated = true;
    }

}
