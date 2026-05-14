using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats enemyStats;

    public float aggroDistance;
    public float attackDistance;
    public float attackDuration;
    private GameObject player;
    public float moveSpeed;
    public int damage;
    public bool isAttacking;
    public bool Walk;
    private Rigidbody2D rb;
    public float jumpForce;
    public LayerMask groundLayer;
    private float jumpOffset = 0.3f;

    [System.Serializable]
    public class LootItem
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float dropChance;
        public int minAmount = 1;
        public int maxAmount = 1;
    }

    public LootItem[] lootTable;

    public Animator anim;

    private void Start()
    {
        moveSpeed = enemyStats.enemySpeed;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
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


    private void DamagePlayer(int damage)
    {
        StatsManager.Instance.currentHealth -= damage;
    }

    private void Die()
    {
        StatsManager.Instance.xp += enemyStats.xpValue;
        DropLoot();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        foreach (var item in lootTable)
        {
            if (Random.value <= item.dropChance)
            {
                //Spawn random amount of loot between min and max for that loot item eg. 1-1 for legendary sword, 5-10 for coins.
                int amount = Random.Range(item.minAmount, item.maxAmount + 1);

                for (int i = 0; i < amount; i++)
                {
                    Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
                    GameObject loot = Instantiate(item.prefab, spawnPos, Quaternion.identity);

                    Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        //Scatter loot with random force
                        Vector2 force = new Vector2(
                            Random.Range(-1f, 1f),
                            Random.Range(1f, 2f)
                        ) * Random.Range(2f, 5f);

                        rb.AddForce(force, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
