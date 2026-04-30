using UnityEngine;
using UnityEngine.SceneManagement;

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
}