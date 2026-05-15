using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRange,
            attackMask
        );

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>()
                .TakeDamage(attackDamage);
        }
    }

    public void EnragedAttack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRange,
            attackMask
        );

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>()
                .TakeDamage(enragedAttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
}