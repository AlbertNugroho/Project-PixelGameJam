using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float AttackDistance = 2f;
    public int AttackDamage = 10;
    public float AttackCooldown = 1f;
    public float timeSinceLastAttack = 0f;
    public Transform attackPoint;
    public GameObject attackEffectPrefab;
    public GameObject target;
    Rigidbody2D rb;
    public Animator arm1;
    public Animator arm2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack -= Time.deltaTime;

        if (timeSinceLastAttack <= 0f)
        {
            float distanceToTarget = Vector2.Distance(rb.position, target.transform.position);
            if (distanceToTarget <= AttackDistance)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        AudioManager.instance.PlayClip(AudioManager.instance.enemyatkfx);
        arm1.SetTrigger("Atk");
        arm2.SetTrigger("Atk");
        GameObject Slash = Instantiate(attackEffectPrefab, attackPoint.position, attackPoint.rotation);
        float scale = Mathf.Min(AttackDamage / 7f, 3f);
        Slash.transform.localScale = Vector2.one * scale;
        Destroy(Slash, 0.2f);
        timeSinceLastAttack = AttackCooldown;
    }
}
