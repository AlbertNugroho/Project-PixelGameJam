using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 1000f;
    public TextMeshProUGUI healthText;
    private float currentHealth;
    public GameObject bossHealthbar;
    public BossAI boss;
    public BossCannon bc;
    public GrabPlayer gp;
    public GrabAttack ga;
    public FacePlayerChar fpc;
    public CanvasGroup wintext;

    public Animator bossAnimator;
    private bool hasWon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;
        checkDeath();
        UpdateHealth();
    }

    public void checkDeath()
    {
        if (hasWon) return;
        if (currentHealth <= 0)
        {
            hasWon = true;
            AudioManager.instance.PlayClip(AudioManager.instance.BossDeath);
            boss.enabled = false;
            bc.enabled = false;
            gp.enabled = false;
            ga.enabled = false;
            fpc.enabled = false;
            boss.rb.linearVelocity = Vector2.zero;
            bossAnimator.SetTrigger("Death");
            bossHealthbar.SetActive(false);
            StartCoroutine(win());
        }
    }

    public IEnumerator win()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(fadeinWintext());
    }
    public IEnumerator fadeinWintext()
    {
        wintext.alpha = 0f;
        wintext.gameObject.SetActive(true);

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            wintext.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }
    private void UpdateHealth()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
