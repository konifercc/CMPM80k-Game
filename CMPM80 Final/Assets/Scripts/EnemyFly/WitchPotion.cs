using UnityEngine;

public class WitchPotion : MonoBehaviour
{
    [SerializeField] private float Damage = 50f;
    [SerializeField] private float lifetime = 1f;
    [SerializeField] public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
            return;
        }
        rb.gravityScale = 0;

        Debug.Log("Destroying game object in " + lifetime + " seconds.");
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerhealth = GameObject.FindWithTag("PlayerTag").GetComponent<PlayerHealth>();

        if (collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("Hit something?");
            playerhealth.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
