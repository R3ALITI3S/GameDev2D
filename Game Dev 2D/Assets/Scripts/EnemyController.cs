using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float aggroDistance;
    public float attackDistance;
    public float attackDuration;
    private GameObject player;
    public float moveSpeed;
    public int damage;
    public bool isAttacking;
    public bool Walk;


    public Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
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
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", false);
        }
    }

    private IEnumerator AttackPlayer(float seconds)
    {
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        DamagePlayer(StatsManager.Instance.enemyDamage);
        isAttacking = false;
    }

    private void DamagePlayer(int damage)
    {
        StatsManager.Instance.currentHealth -= StatsManager.Instance.enemyDamage;
    }
}
