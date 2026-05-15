using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed = 10f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;

    private float nextAttackTime;
    private bool isAttacking;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("ENTER RUN STATE");

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

        isAttacking = false;

        if (player == null) Debug.LogError("PLAYER NOT FOUND!");
        if (rb == null) Debug.LogError("RIGIDBODY2D NOT FOUND!");
        if (boss == null) Debug.LogError("BOSS SCRIPT NOT FOUND!");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || rb == null || boss == null)
            return;

        float distance = Vector2.Distance(rb.position, player.position);

        // Only flip when NOT attacking
        if (!isAttacking)
        {
            boss.LookAtPlayer();
        }

        if (distance > attackRange)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (!isAttacking && Time.time >= nextAttackTime)
            {
                isAttacking = true;
                animator.SetTrigger("StartAttack");
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false;

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}