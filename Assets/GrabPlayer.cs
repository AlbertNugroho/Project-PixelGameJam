using System.Collections;
using UnityEngine;

public class GrabPlayer : MonoBehaviour
{
    bool isGrabbing = false;
    public Transform target;
    public Collider2D center;
    public Transform Boss;
    public BossAI bossAI;
    public BossCannon bossCannon;
    public Animator bosAnim;
    public Animator handAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbing)
        {
            target.GetComponent<Rigidbody2D>().MovePosition(center.bounds.center);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (collision.GetComponent<Dash>().isDashing || FindFirstObjectByType<Health>().isImmune)
            {
                return;
            }
            AudioManager.instance.PlayClip(AudioManager.instance.bossGrabHit);
            bosAnim.SetTrigger("Idle");
            collision.GetComponent<PlayerMove>().enabled = false;
            collision.GetComponent<Dash>().enabled = false;
            isGrabbing = true;
            bossCannon.enabled = false;
            target = collision.transform;
            handAnim.SetTrigger("Throw");
        }
    }

    public void EnableAI()
    {
        if(isGrabbing == true)
            return;
        bosAnim.SetTrigger("Walk");
    }

    public void toggleCollider()
    {
        center.enabled = !center.enabled;
    }
    public void ReleasePlayer()
    {
        if (target == null) return;

        var playerMove = target.GetComponent<PlayerMove>();
        var dash = target.GetComponent<Dash>();
        var rb = playerMove?.rb;

        if (rb != null)
        {
            isGrabbing = false;
            Vector2 throwDirection = Boss.GetComponent<BossAI>().isRight ? Vector2.right : Vector2.left;
            AudioManager.instance.PlayClip(AudioManager.instance.bossThrow);
            rb.linearVelocity = throwDirection * 10f;
            StartCoroutine(StopPlayerAfterDelay(rb, 0.5f));
        }
        target = null;
    }

    private IEnumerator StopPlayerAfterDelay(Rigidbody2D rb, float delay)
    {
        var playerMove = target.GetComponent<PlayerMove>();
        var dash = target.GetComponent<Dash>();
        yield return new WaitForSeconds(delay);
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
        if (playerMove != null) playerMove.enabled = true;
        if (dash != null) dash.enabled = true;
        yield return new WaitForSeconds(0.1f);
        FindFirstObjectByType<Health>()?.TakeDamage(20);
        bosAnim.SetTrigger("Walk");
    }
}
