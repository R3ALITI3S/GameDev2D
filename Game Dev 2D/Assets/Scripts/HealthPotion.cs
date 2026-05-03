using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FullHealth();
            Destroy(gameObject);
        }
    }

    public void FullHealth()
    {
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
    }
}
