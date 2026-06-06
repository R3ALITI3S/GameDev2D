using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string defaultScene;

    public void LoadDefault() => SceneManager.LoadScene(defaultScene); // timeline

    public void Load(string scene) => SceneManager.LoadScene(scene); // Button press
}