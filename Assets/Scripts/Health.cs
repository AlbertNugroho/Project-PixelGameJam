using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject deathScreen;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 10f;
    public TextMeshProUGUI healthText;
    private float currentHealth;
    public bool isImmune;
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

    public void TakeDamage(int amount)
    {
        AudioManager.instance.PlayClip(AudioManager.instance.Damagedfx);
        currentHealth -= amount;
        healthBar.value = currentHealth;
        CheckLoseCondition();
        UpdateHealth();
        isImmune = true;
        Invoke("disableImune", 1f);
    }
    void disableImune()
    {
        isImmune = false;
    }
    public void FullHeal()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealth();
    }

    private void CheckLoseCondition()
    {
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlayClip(AudioManager.instance.deathfx);
            AudioManager.instance.PlayMusic(AudioManager.BgAudio.Death);
            PauseManager.Instance.PauseMovement();
            deathScreen.SetActive(true);
            IsBusy.singleton.isBusy = true;
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
