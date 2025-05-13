using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 10f;
    public TextMeshProUGUI healthText;
    private float currentHealth;
    private void Awake()
    {
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealth();
    }

    public void TakeDamage(float amount = 1f)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;
        CheckLoseCondition();
        UpdateHealth();
    }

    private void CheckLoseCondition()
    {
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Player Dead");
        }
    }
    private void UpdateHealth()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.value = currentHealth;
        UpdateHealth();
    }

}
