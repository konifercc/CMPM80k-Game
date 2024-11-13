using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameController gameController;
    public Transform respawnPoint;

    private void Awake(){
        gameController = GameObject.FindWithTag("PlayerTag").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("PlayerTag")){
            gameController.updateCheckpoint(respawnPoint.transform.position);
            Debug.Log("works");
        }
    }


}
