using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float enemyMinHealth;
    public float enemyMaxHealth;
    public float enemyHealth;
    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageE(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
