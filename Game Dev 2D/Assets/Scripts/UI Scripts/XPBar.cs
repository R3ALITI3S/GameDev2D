using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class XPBar : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;      // Displays the current level number

    private int lastLevel = -1;

    void OnValidate()
    {
        // Keep slider configured for normalized [0,1] operation in editor
        if (xpSlider != null)
        {
            xpSlider.minValue = 0f;
            xpSlider.maxValue = 1f;
            xpSlider.wholeNumbers = false;
        }
    }

    void Start()
    {
        if (xpSlider != null)
        {
            xpSlider.minValue = 0f;
            xpSlider.maxValue = 1f;
            xpSlider.wholeNumbers = false;
        }
    }

    void Update()
    {
        if (xpSlider == null || levelText == null)
        {
            return;
        }

        if (StatsManager.Instance == null)
        {
            levelText.text = "Level 1";
            xpSlider.value = 0f;
            return;
        }

        int xp = StatsManager.Instance.xp;
        int level = StatsManager.Instance.level;

        // Determine level bounds based on the same thresholds used in StatsManager
        int[] levelMax = new int[] { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500, int.MaxValue };

        int minXp;
        if (level == 1)
        {
            minXp = 0;
        }
        else
        {
            minXp = levelMax[level - 2] + 1;
        }

        int maxXp = levelMax[Mathf.Clamp(level - 1, 0, levelMax.Length - 1)];

        // If level changed, reset the bar visually before setting the new fill
        if (lastLevel != level)
        {
            xpSlider.value = 0f;
            lastLevel = level;
        }

        // Compute fill for the current level (handle open-ended max for highest level)
        int currentOffset = Mathf.Max(0, xp - minXp);

        float denom; // Represents the XP range for the current level
        int denomInt;
        if (maxXp == int.MaxValue)
        {
            denom = 1f;
            denomInt = -1; // sentinel to indicate open-ended
        }
        else
        {
            denom = (float)(maxXp - minXp);
            denomInt = maxXp - minXp;
        }

        float fill;
        if (denom <= 0f)
        {
            fill = 0f;
        }
        else
        {
            fill = Mathf.Clamp01(currentOffset / denom);
        }

        // Use normalized slider (0..1) so behavior matches previous Image.fillAmount usage
        xpSlider.value = fill;

        // Update text fields: show level
        levelText.text = $"Level {level}";
    }
}