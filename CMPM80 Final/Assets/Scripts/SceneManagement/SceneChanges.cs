using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour
{

    [SerializeField] private int sceneIndex;
    [SerializeField] private Vector3 travelLocation;
    private GameObject player;

    void Start(){
        //player = FindGetComponent<Player>();
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerTag"){
            //Debug.Log("portal");
             SceneManager.LoadScene(sceneIndex);
             player.transform.position = travelLocation;
        }
    }
}
