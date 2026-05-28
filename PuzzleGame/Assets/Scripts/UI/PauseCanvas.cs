using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public Canvas pauseCanvas;
    public GameObject player;

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
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
