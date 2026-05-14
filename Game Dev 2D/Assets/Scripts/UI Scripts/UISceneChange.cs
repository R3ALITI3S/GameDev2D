using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISceneChange : MonoBehaviour
{
    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
