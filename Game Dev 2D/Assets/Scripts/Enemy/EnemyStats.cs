using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Player Combat Stats")]
    public int enemyDamage;

    [Header("Enemy Health Stats")]
    public int enemyMaxHealth;
    public int enemyCurrentHealth;

    [Header("Enemy Movement Stats")]
    public float enemySpeed;

    [Header("MISC")]
    public int xpValue;

}
