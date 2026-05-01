using Unity.VisualScripting;
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

    [Header("MISC")]
    public int xp;
    public int level;

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

    void Update()
    {
        if (currentHealth <= 0)
        {
            //ResetGame();
        }

        if (xp <= 100)
        {
            level = 1;
        }
        else if (xp > 100 && xp <= 300)
        {
            level = 2;
        }
        else if (xp > 300 && xp <= 600)
        {
            level = 3;
        }
        else if (xp > 600 && xp <= 1000)
        {
            level = 4;
        }
        else if (xp > 1000)
        {
            level = 5;
        }
    }

}
