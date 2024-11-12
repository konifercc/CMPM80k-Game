using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth {get; private set;}
    public bool isAttacked;
    public bool recieveFallDamage;

    void Start()
    {
        playerHealth = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
