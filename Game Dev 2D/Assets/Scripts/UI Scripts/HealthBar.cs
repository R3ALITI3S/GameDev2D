using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[ExecuteInEditMode] //Makes the script execute in edit mode, allowing you to see changes in the editor without entering play mode
public class HealthBar : MonoBehaviour
{
    public int minimum;
    public Image maskImage;

    // Optional TextMeshPro field to show "current / max" health
    public TMP_Text healthText;

    // These constants define the range of fill amounts for the health bar, allowing for a visual buffer at the start and end of the bar.
    private const float MinFill = 0.125f;
    private const float MaxFill = 0.875f;


    public void Update()
    {
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        if (StatsManager.Instance == null || maskImage == null)
            return;

        float currentOffset = StatsManager.Instance.currentHealth - minimum;
        float maximumOffset = StatsManager.Instance.maxHealth - minimum;

        // Avoid divide-by-zero
        if (maximumOffset <= 0f)
            return;

        float fillAmount = currentOffset / maximumOffset;

        // Clamp then remap from [0,1] to [MinFill,MaxFill]
        float clamped = Mathf.Clamp01(fillAmount);
        maskImage.fillAmount = Mathf.Lerp(MinFill, MaxFill, clamped);

        // Update health text
        if (healthText != null)
        {
            int current = Mathf.Clamp(StatsManager.Instance.currentHealth, minimum, StatsManager.Instance.maxHealth);
            int max = StatsManager.Instance.maxHealth;

            healthText.SetText("{0} / {100}", current, max);
        }
    }

}
