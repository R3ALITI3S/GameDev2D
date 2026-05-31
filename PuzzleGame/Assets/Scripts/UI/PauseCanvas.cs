using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public Canvas pauseCanvas;
    public GameObject player;

    [SerializeField] RectTransform fader; //Transition image

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(PauseCanvas);
    }

    void Start()
    {
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.enabled)
            {
                ResumeGame();
                return;
            }
            OpenPauseCanvas();
        }
    }

    public void OpenPauseCanvas()
    {
        if (pauseCanvas == null)
        {
            return;
        }
        pauseCanvas.enabled = true;
        player.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        if (pauseCanvas == null)
        {
            return;
        }
        pauseCanvas.enabled = false;
        player.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        pauseCanvas.enabled = false;
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadMenu", 0.5f);
        });
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadLevel()
    {
        pauseCanvas.enabled = false;
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LevelReloading", 0.5f);
        });
    }

    private void LevelReloading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
