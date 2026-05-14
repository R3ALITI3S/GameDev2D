using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class sceneChange : MonoBehaviour
{
    
    [SerializeField] private int sceneBuildIndex;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player 
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }

    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Upgrade Scene");
        }
    }
}