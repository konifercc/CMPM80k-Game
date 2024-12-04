using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GuideScript : MonoBehaviour
{
    public GameObject player;
    private float triggerDistance = 3;
    public GameObject popUp;
    public GameObject popUp2;
    public GameObject popUp3;
    public Animator playerAnim;
    public float promptCount = 0;
    public float typingSpeed = 0.1f;
    private float encounterC = 0;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        if (player == null)
        {
            Debug.Log("Player not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && promptCount == 1)
        {
            popUp.SetActive(false);
            popUp2.SetActive(true);
            //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //playerAnim.SetTrigger("isJumping");
            //isPrompted = false;
        }
     
        if (Input.GetKeyDown(KeyCode.E) && promptCount == 2)
        {
            Debug.Log("prompt3");
            popUp2.SetActive(false);
            popUp3.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && promptCount == 3)
        {
            popUp3.SetActive(false);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (Input.GetKeyUp(KeyCode.E) && promptCount >= 1)
        {
            promptCount++;
        }
    

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag") && encounterC == 0)
        {
            Debug.Log("Player entered trigger");
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //Animator playerAnim = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Animator>();
            popUp.SetActive(true);
            promptCount++;
            encounterC++;
        }
    }

    /*
    IEnumerator RevealText(string fullText)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textMeshPro.text = fullText.Substring(0, i); // Display substring up to current index
            yield return new WaitForSeconds(typingSpeed); // Wait for the next letter to appear
        }
    }
    */
}
