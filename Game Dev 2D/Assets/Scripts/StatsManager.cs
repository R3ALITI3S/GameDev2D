using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance; // Singleton instance

    // ------- Player Stats -------
    [Header("Player Combat Stats")]
    public float damage;
    public float defense;

    [Header("Player Health Stats")]
    public float maxHealth;
    public float currentHealth;

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
        else if (xp > 1000 && xp <= 1500)
        {
            level = 5;
        }
        else if (xp > 1500 && xp <= 2100)
        {
            level = 6;
        }
        else if (xp > 2100 && xp <= 2800)
        {
            level = 7;
        }
        else if (xp > 2800 && xp <= 3600)
        {
            level = 8;
        }
        else if (xp > 3600 && xp <= 4500)
        {
            level = 9;
        }
        else if (xp > 4500)
        {
            level = 10;
        }
    }

}
