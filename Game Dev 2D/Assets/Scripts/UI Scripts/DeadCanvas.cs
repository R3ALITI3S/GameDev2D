using UnityEngine;

public class DeadCanvas : MonoBehaviour
{
    public GameObject deadCanvas;
    public GameObject normalCanvas;

    private void Start()
    {
        deadCanvas.SetActive(false);
        normalCanvas.SetActive(true);
    }

    private void Update()
    {
        if (StatsManager.Instance.currentHealth <= 0)
        {
            deadCanvas.SetActive(true);
            normalCanvas.SetActive(false);
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth; // Reset health when restarting
    }
}
