using Pathfinding;
using Unity.AppUI.Core;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public GameObject enemyGFX;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;

    public Animator animator;
    SpriteRenderer sp;
    public float AttackDistance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sp = enemyGFX.GetComponent<SpriteRenderer>();
        
        InvokeRepeating("UpdatePath", 0f, 0.2f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(rb.position, target.position);
        if (distanceToTarget < AttackDistance)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.linearVelocity = direction * speed;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < AttackDistance)
        {
            currentWaypoint++;
            return;
        }

        if (rb.linearVelocity.x < 0)
            sp.flipX = true;
        else if (rb.linearVelocity.x > 0)
            sp.flipX = false;

        animator.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.01f);
    }

}
