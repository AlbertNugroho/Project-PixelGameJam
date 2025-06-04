using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    private float timer;
    private bool triggered;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        triggered = false;
        
        BossAI bossAI = animator.GetComponent<BossAI>();
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                bossAI.bossCannon.enabled = false;
                break;
            case 1:
                bossAI.bossCannon.enabled = true;
                break;
            case 2:
                bossAI.bossCannon.enabled = true;
                break;
        }
        bossAI.rb.linearVelocity = Vector2.zero;
        bossAI.speed = 0f;
        bossAI.AttackDistance = 2;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (triggered) return;

        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            triggered = true;
            TriggerRandomState(animator);
        }
    }

    void TriggerRandomState(Animator animator)
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                animator.SetTrigger("Run");
                break;
            case 1:
                animator.SetTrigger("Walk");
                break;
        }
    }
}
