using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] RectTransform fader; //Transition image

    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0); //Set the fader to maximum size
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false); //Disable the fader after the transition is complete
        });
    }

    public void GoToMainMenu()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadMenu", 0.5f);
        });
    }

    public void GoToLvl1()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl1", 0.5f);
        });
    }

    public void GoToLvl2()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl2", 0.5f);
        });
    }
    public void GoToLvl3()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl3", 0.5f);
        });
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadLvl1()
    {
        SceneManager.LoadScene("Lvl 1");
    }

    private void LoadLvl2()
    {
        SceneManager.LoadScene("Lvl 2");
    }

    private void LoadLvl3()
    {
        SceneManager.LoadScene("Lvl 3");
    }
}
