using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.SceneManagement;

public class Rift : MonoBehaviour
{

    private ParticleSystem particleSystem;

    private GameObject player;
    private GameObject canvases;
    private float triggerDistance = 3;
    public GameObject popUp;
    private bool canFinish = false;

    public GameObject sparks;

    public GameObject newSparks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        canvases = GameObject.FindGameObjectWithTag("Canvases");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canFinish)
        {
            popUp.SetActive(false);

            StartCoroutine(riftClose());
        }
     
    }

    IEnumerator riftClose(){
        yield return new WaitForSeconds(1f);
        Destroy(sparks);
        yield return new WaitForSeconds(1f);
        Instantiate(newSparks, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(newSparks);
        yield return new WaitForSeconds(1f);
        Destroy(player);
        Destroy(canvases);
        SceneManager.LoadSceneAsync(0);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
        {
            Debug.Log("Player entered trigger");
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //Animator playerAnim = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Animator>();
            popUp.SetActive(true);
            canFinish = true;
        }
    }
}
