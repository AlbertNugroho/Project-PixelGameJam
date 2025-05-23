using Pathfinding;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public GameObject enemyGFX;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    public Rigidbody2D rb;
    Collider2D circleCollider2D;
    public bool isRight =true;
    public Animator animator;
    public BossCannon bossCannon;
    SpriteRenderer sp;
    public float AttackDistance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sp = enemyGFX.GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<Collider2D>();
        InvokeRepeating("UpdatePath", 0f, 0.2f);
        Physics2D.IgnoreCollision(circleCollider2D, target.GetComponent<Collider2D>(), true);
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

        // Flip using local scale instead of SpriteRenderer.flipX
        if (direction.x < 0)
        {
            enemyGFX.transform.localScale = new Vector3(-Mathf.Abs(enemyGFX.transform.localScale.x), enemyGFX.transform.localScale.y, enemyGFX.transform.localScale.z);
            isRight = false;
        }
        else if (direction.x > 0)
        {
            enemyGFX.transform.localScale = new Vector3(Mathf.Abs(enemyGFX.transform.localScale.x), enemyGFX.transform.localScale.y, enemyGFX.transform.localScale.z);
            isRight = true;
        }

    }

}
