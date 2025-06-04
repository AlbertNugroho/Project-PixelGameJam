using UnityEngine;

public class RunBehavior : StateMachineBehaviour
{
    private float timer = 0f;
    private bool triggered = false;

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
        bossAI.AttackDistance = 1f;
        bossAI.enabled = true;
        bossAI.speed = 7;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (triggered) return;

        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            triggered = true;
            int randomChoice = Random.Range(0, 2);
            if (randomChoice == 0)
                animator.SetTrigger("Walk");
            else
                animator.SetTrigger("Idle");
        }
    }
}
