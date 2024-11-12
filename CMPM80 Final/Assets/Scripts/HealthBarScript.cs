using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slider;
    public float health;

    public void setSliderHealth(float playerHealth)
    {
        slider.value = playerHealth;
    }

    void Start()
    {
        setSliderHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        setSliderHealth(health);
    }
}
