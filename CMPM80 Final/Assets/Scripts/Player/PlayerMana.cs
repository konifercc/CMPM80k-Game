using System.Collections;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float CurrentMana { get; private set; }
    [SerializeField] private float maxMana = 100.0f; // Properly set in Inspector or here
    public float MaxMana => maxMana; // Read-only property
    public float MinMana { get; private set; } = 0.0f;

    [Header("Mana Settings")]
    [SerializeField] private float manaRegenRate = 5.0f; // Mana regenerated per second
    [SerializeField] private float regenDelay = 2.0f; // Time before regeneration starts
    [SerializeField] private float fireballMinMana = 50.0f; // Minimum mana required to shoot fireball

    [Header("UI Reference")]
    [SerializeField] private ManaBarScript manaBar;

    private bool isRegenerating = false;
    private float lastManaUseTime = 0f;

    private void Awake()
    {
        if (manaBar == null)
        {
            manaBar = GameObject.FindWithTag("ManaBarTag")?.GetComponent<ManaBarScript>();
        }
    }

    private void Start()
    {
        CurrentMana = maxMana;
        if (manaBar != null)
        {
            manaBar.setMaxMana(maxMana);
        }
    }

    private void Update()
    {
        manaBar?.setMana(CurrentMana);

        CurrentMana = Mathf.Clamp(CurrentMana, MinMana, maxMana);

        if (!isRegenerating && Time.time - lastManaUseTime >= regenDelay && CurrentMana < maxMana)
        {
            StartCoroutine(RegenerateMana());
        }
    }

    public bool UseMana(float manaCost)
    {
        if (CurrentMana >= manaCost)
        {
            CurrentMana -= manaCost;
            lastManaUseTime = Time.time;
            isRegenerating = false;
            StopAllCoroutines();
            return true;
        }

        return false;
    }

    public void AddMana(float manaAmount)
    {
        CurrentMana += manaAmount;
        CurrentMana = Mathf.Clamp(CurrentMana, MinMana, maxMana);
    }

    public void SetMaxMana()
    {
        CurrentMana = maxMana;
        Debug.Log("Mana set to max.");
    }

    private IEnumerator RegenerateMana()
    {
        isRegenerating = true;

        while (CurrentMana < maxMana)
        {
            CurrentMana += manaRegenRate * Time.deltaTime;
            CurrentMana = Mathf.Min(CurrentMana, maxMana);
            manaBar?.setMana(CurrentMana);
            yield return null;
        }

        isRegenerating = false;
    }
}