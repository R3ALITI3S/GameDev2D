using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button buttonContinue;

    public GameObject CheckBox;

    void Start()
    {
        buttonContinue = GetComponent<Button>();
        CheckBox.SetActive(false);
    }
    public void showContinueButton()
    {
        gameObject.SetActive(true);
        buttonContinue.interactable = true;
        CheckBox.SetActive(true);
    }

    public void HideButton()
    {
        buttonContinue.interactable = false;
        buttonContinue.gameObject.SetActive(false);
        CheckBox.SetActive(false);
    }
}
