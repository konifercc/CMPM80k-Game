using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour
{
    public Slider manaSlider; // Reference to the Slider UI component

    public void setMaxMana(float maxMana)
    {
        if (manaSlider == null)
        {
            Debug.LogError("Mana Slider is not assigned!");
            return;
        }

        manaSlider.maxValue = maxMana;
        manaSlider.value = maxMana; // Initialize the slider value
    }

    public void setMana(float mana)
    {
        if (manaSlider == null)
        {
            Debug.LogError("Mana Slider is not assigned!");
            return;
        }

        manaSlider.value = mana; // Update the slider value
    }
}