using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLvl1()
    {
        SceneManager.LoadScene("Lvl 1");
    }

    public void GoToLvl2()
    {
        SceneManager.LoadScene("Lvl 2");
    }
    public void GoToLvl3()
    {
        SceneManager.LoadScene("Lvl 3");
    }
}
