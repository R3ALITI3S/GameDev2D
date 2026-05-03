using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class XPBar : MonoBehaviour
{
    public Image maskImage;
    public TextMeshProUGUI levelText;      // Displays the current level number
    public TextMeshProUGUI levelXpText;    // Displays "current XP in level / XP required for level"

    private int lastLevel = -1;

    void Update()
    {
        if (maskImage == null || levelText == null || levelXpText == null)
        {
            return;
        }

        if (StatsManager.Instance == null)
        {
            levelText.text = "1";
            levelXpText.text = "100 / 100";
            maskImage.fillAmount = 0f;
            return;
        }

        int xp = StatsManager.Instance.xp;
        int level = StatsManager.Instance.level;

        // Determine level bounds based on the same thresholds used in StatsManager
        int[] levelMax = new int[] { 100, 300, 600, 1000, int.MaxValue };

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
            maskImage.fillAmount = 0f;
            lastLevel = level;
        }

        // Compute fill for the current level (handle open-ended max for highest level)
        int currentOffset = Mathf.Max(0, xp - minXp);

        float denom; //Represents the XP range for the current level
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

        maskImage.fillAmount = fill;

        // Update text fields: show level number and xp progress within the current level
        levelText.text = level.ToString();

        if (denomInt == -1)
        {
            levelXpText.text = currentOffset.ToString() + " / -"; // Show when at the highest level
        }
        else
        {
            levelXpText.text = currentOffset.ToString() + " / " + denomInt.ToString();
        }
    }
}