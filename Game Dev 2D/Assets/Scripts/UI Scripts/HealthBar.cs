using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode] //Makes the script execute in edit mode, allowing you to see changes in the editor without entering play mode
public class HealthBar : MonoBehaviour
{
    public int minimum;
    public Image maskImage;


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
        float fillAmount = currentOffset / maximumOffset;
        maskImage.fillAmount = fillAmount;
    }

}
