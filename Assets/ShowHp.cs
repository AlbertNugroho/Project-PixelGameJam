using UnityEngine;
using UnityEngine.UI;

public class ShowHp : MonoBehaviour
{
    public GameObject HpBar;
    public Slider slider;
    public GeneralHealth health;
    public int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health.Health;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth()
    {
        slider.value = health.Health;
        HpBar.SetActive(health.Health < maxHealth);
    }

}
