using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;

    Transform player;
    Rigidbody2D rb;
    Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{

    boss.LookAtPlayer();
    Debug.Log("player look!");
    if (player == null || rb == null) return;

    Vector2 target = new Vector2(player.position.x, rb.position.y);
    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);

    rb.MovePosition(newPos);
    Debug.Log("We movin");

    if (Vector2.Distance(player.position, rb.position) <= attackRange)
    {
        animator.SetTrigger("Attack");
    }
}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    
}
