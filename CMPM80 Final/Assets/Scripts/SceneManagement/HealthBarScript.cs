using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slider;
    //public float health; for testing
    public float sliderMaxHealth;

    public void setHealth(float playerHealth)
    {
        slider.value = playerHealth;
    }

    public void setMaxHealth(float playerMaxHealth)
    {
        slider.value = playerMaxHealth;
        slider.maxValue = playerMaxHealth;
    }

    void Start()
    {
        //If you choose to modify the bar through this script
        /*
        health = 100.0f;
        setMaxHealth(health);
        */
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //If you choose to modify the bar through this script
        /*
        health -= 0.05f;
        setHealth(health);
        Debug.LogError(health);
        */
    }
}
