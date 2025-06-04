using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public Animator animator;
    public int AttackDamage = 10;
    public float AttackCooldown = 1f;
    private float timeSinceLastAttack = 0f;
    public float AttackDistance = 2f;
    public Transform attackPoint;
    public GameObject attackEffectPrefab;
    public LayerMask enemyLayer;

    void Update()
    {
        timeSinceLastAttack -= Time.deltaTime;

        if (timeSinceLastAttack <= 0f)
        {
            // Check if any enemy is in range
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, AttackDistance, enemyLayer);
            if (hit != null)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Atk");
        GameObject slash = Instantiate(attackEffectPrefab, attackPoint.position, attackPoint.rotation);
        slash.transform.localScale = Vector2.one * Mathf.Min(AttackDamage / 7f, 3f);
        Destroy(slash, 0.2f);
        timeSinceLastAttack = AttackCooldown;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackDistance, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            GeneralHealth health = enemy.GetComponent<GeneralHealth>();
            if (health != null)
            {
                health.takeDamage(AttackDamage);
            }
        }
    }
}
