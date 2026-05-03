using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private EnemyStats enemyStats;

    private void Awake()
    {
        // Use a normalized slider (0..1)
        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    private void Update()
    {
        slider.value = (float)enemyStats.enemyCurrentHealth / (float)enemyStats.enemyMaxHealth;
    }
}   
