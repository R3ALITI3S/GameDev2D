using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats enemyStats;
    public StatsUpgrade statsUpgrade;

    public float aggroDistance;
    public float attackDistance;
    public float attackDuration;
    private GameObject player;
    public float moveSpeed;
    public float damage;
    public bool isAttacking;
    public bool Walk;
    private Rigidbody2D rb;
    public float jumpForce;
    public LayerMask groundLayer;
    private float jumpOffset = 0.3f;

    private float playerDefense = 1f;
    private float upgradeDefense = 1f;


    public Animator anim;

    private void Start()
    {
        moveSpeed = enemyStats.enemySpeed;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();

        StatsManager.Instance.defense = playerDefense;
        statsUpgrade.upgradeDefenseAmount = upgradeDefense;
    }
    void Update()
    {
        //Get walk direction to set origin of raycast
        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
        Vector2 origin = new Vector2(
            transform.position.x + direction * 0.7f,
            transform.position.y + jumpOffset
        );

        //Cast ray to check for ledge
        float rayDistance = 0.4f;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, rayDistance, groundLayer);

        //Jump if at ledge
        if (hit.collider != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //Attack if within attack distance, else move towards player if within aggro distance
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance <= attackDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer(attackDuration));
                anim.SetBool("Walk", true);
            }
        }
        else if (playerDistance <= aggroDistance && playerDistance >= attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", false);
        }

        if (enemyStats.enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator AttackPlayer(float seconds)
    {
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance <= attackDistance)
        {
            DamagePlayer(enemyStats.enemyDamage);
        }
        isAttacking = false;
    }


    private void DamagePlayer(float damage)
    {
        StatsManager.Instance.currentHealth -= damage - (playerDefense * upgradeDefense * 0.1f);
    }

    private void Die()
    {
        StatsManager.Instance.xp += enemyStats.xpValue;
        Destroy(gameObject);
    }
}
