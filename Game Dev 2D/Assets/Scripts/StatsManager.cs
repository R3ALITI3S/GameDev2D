using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance; // Singleton instance

    // ------- Player Stats -------
    [Header("Player Combat Stats")]
    public int damage;
    public int defense;

    [Header("Player Health Stats")]
    public int maxHealth;
    public int currentHealth;

    [Header("Player Movement Stats")]
    public float speed;
    public float jumpForce;

    // ------- Enemy Stats -------
    [Header("Player Combat Stats")]
    public int enemyDamage;

    [Header("Enemy Health Stats")]
    public int enemyMaxHealth;
    public int enemyCurrentHealth;

    [Header("Enemy Movement Stats")]
    public float enemySpeed;

    private void Awake()
    {
        // Implementing Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}
