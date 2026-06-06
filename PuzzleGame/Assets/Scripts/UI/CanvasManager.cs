using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] RectTransform fader; //Transition image

    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0); //Set the fader to maximum size
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => //Transition fades away as the scene starts
        {
            fader.gameObject.SetActive(false); //Disable the fader after the transition is complete
        });
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EndScene" && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //Quit the game if the player is on the end scene
        }
    }

    public void GoToMainMenu()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadMenu", 0.5f); //Load the main menu after the transition is complete
        });
    }

    public void GoToLevelSelect()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLevelSelect", 0.5f); //Load the level menu after the transition is complete
        });
    }

    public void GoToIntro()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadIntro", 0.5f); //Load the level menu after the transition is complete
        });
    }

    public void GoToLvl0()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl0", 0.5f);
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

    public void GoToLvl4()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl4", 0.5f);
        });
    }

    public void GoToLvl5()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl5", 0.5f);
        });
    }

    public void GoToLvl6()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl6", 0.5f);
        });
    }

    public void GoToLvl7()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl7", 0.5f);
        });
    }

    public void GoToLvl8()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl8", 0.5f);
        });
    }

    public void GoToLvl9()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl9", 0.5f);
        });
    }

    public void GoToLvl10()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl10", 0.5f);
        });
    }

    public void GoToLvl11()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvl11", 0.5f);
        });
    }

    public void GoToLvlEndScene()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLvlEndScene", 0.5f);
        });
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    private void LoadLvl0()
    {
        SceneManager.LoadScene("Lvl 0");
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

    private void LoadLvl4()
    {
        SceneManager.LoadScene("Lvl 4");
    }

    private void LoadLvl5()
    {
        SceneManager.LoadScene("Lvl 5");
    }

    private void LoadLvl6()
    {
        SceneManager.LoadScene("Lvl 6");
    }

    private void LoadLvl7()
    {
        SceneManager.LoadScene("Lvl 7");
    }
    private void LoadLvl8()
    {
        SceneManager.LoadScene("Lvl 8");
    }
    private void LoadLvl9()
    {
        SceneManager.LoadScene("Lvl 9");
    }

    private void LoadLvl10()
    {
        SceneManager.LoadScene("Lvl 10");
    }
    private void LoadLvl11()
    {
        SceneManager.LoadScene("Lvl 11");
    }
    private void LoadLvlEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
