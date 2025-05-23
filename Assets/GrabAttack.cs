using UnityEngine;

public class GrabAttack : MonoBehaviour
{
    public Animator anim;
    public Collider2D AttackRange;
    public BossAI bossAI;
    public BossCannon bossCannon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossCannon.enabled = false;
            if (collision.transform.position.y > AttackRange.bounds.center.y)
            {
                AudioManager.instance.PlayClip(AudioManager.instance.bossGrab);
                anim.SetTrigger("GrabUp");
            }
            else
            {
                AudioManager.instance.PlayClip(AudioManager.instance.bossGrab);
                anim.SetTrigger("GrabDown");
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
