using UnityEngine;


public class SceneScript : MonoBehaviour
{
    public AudioClip ambientSound;
    private AudioSource audioSource;
    public float nextPlayTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Ambient").GetComponent<AudioSource>();
        nextPlayTime = Time.time + Random.Range(15f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nextPlayTime);
        // Debug.Log(Time.time);
        if (Time.time >= nextPlayTime)
        {
            PlayAmbientSound();
            nextPlayTime = Time.time + Random.Range(15f, 25f);
        }
    }

    private void PlayAmbientSound()
    {
        audioSource.PlayOneShot(ambientSound);
    }
}
