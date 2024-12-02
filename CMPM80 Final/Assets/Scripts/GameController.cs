using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;
    PlayerHealth playerhealth;
    PlayerMovement playerMovement;
    MainMenuScript mainMenuScript;
    GameObject pauseMenu;
    float xScale;
    public bool isPaused;

    public AudioSource audioSource1;
    public AudioSource audioSource3;
    public AudioSource audioSource4;

    public AudioClip deathsound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip runSound;

    private void Start()
    {
        pauseMenu = GameObject.Find("PauseUI");
        checkPointPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerhealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        mainMenuScript = GetComponent<MainMenuScript>();
        isPaused = false;
        pauseMenu.SetActive(false);

        //audioSource1 = GameObject.FindGameObjectWithTag("JumpSound").GetComponent<AudioSource>();
        //audioSource2 = GameObject.FindGameObjectWithTag("LandSound").GetComponent<AudioSource>();
        //audioSource3 = GameObject.FindGameObjectWithTag("RunSound").GetComponent<AudioSource>();
        //audioSource4 = GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            StartCoroutine(Respawn(0.5f));
        }
    }

    public void updateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    public IEnumerator Respawn(float respawnCooldown)
    {
        playDeath();
        playerhealth.isRespawning = true;
        playerhealth.SetMaxHealth();
        rb.linearVelocity = new Vector2(0, 0);
        if (transform.localScale.x < 0)
        {
            playerMovement.IsFacingRight = false;
            playerMovement.Turn();
            Debug.Log(" wrong way " + playerMovement.IsFacingRight);
        }
        rb.simulated = false;
        transform.localScale = new Vector3(0, 0, 1);
        yield return new WaitForSeconds(respawnCooldown);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        rb.simulated = true;
        playerhealth.isRespawning = false;

    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pauses the game
                                 // Additional code to show pause menu, etc.
            pauseMenu.SetActive(true);
            playerMovement.enabled = false;
        }
        else
        {
            Time.timeScale = 1f; // Resumes the game
                                 // Additional code to hide pause menu, etc.
            pauseMenu.SetActive(false);
            playerMovement.enabled = true;
        }
    }

    public void playDeath()
    {

        if (!audioSource4.isPlaying)
        {
            audioSource4.PlayOneShot(deathsound);
        }
    }

    public void playJump()
    {
        audioSource1.PlayOneShot(jumpSound);
    }


    public void playRun()
    {
        audioSource3.clip = runSound;
        audioSource3.loop = true;
        if (!audioSource3.isPlaying)
        {
            audioSource3.Play();
        }
    }

    public void stopRun()
    {
        audioSource3.Stop();
    }
}
