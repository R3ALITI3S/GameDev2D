using UnityEngine;
using TMPro;

public class TMPText : MonoBehaviour
{
    public TextMeshProUGUI output;

    // Update is called once per frame
    void Update()
    {
        output.text = "Energy: ";
    }
}
