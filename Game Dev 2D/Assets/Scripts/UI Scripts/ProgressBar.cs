using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] //Makes the script execute in edit mode, allowing you to see changes in the editor without entering play mode
public class ProgressBar : MonoBehaviour
{
    public int currentProgress;
    public int minimum;
    public int maximim;
    public Image maskImage;


    public void Update()
    {
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        float currentOffset = currentProgress - minimum;
        float maximumOffset = maximim - minimum;
        float fillAmount = currentOffset / maximumOffset;
        maskImage.fillAmount = fillAmount;
    }

}
