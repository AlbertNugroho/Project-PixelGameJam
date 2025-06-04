using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public EnemyAttack EnemyAttack;
    bool hasdamaged = false;
    public Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindFirstObjectByType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasdamaged == false && !other.GetComponent<Dash>().isDashing && !playerHealth.isImmune)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(EnemyAttack.AttackDamage);
                hasdamaged = true;
            }
        }
    }
}
