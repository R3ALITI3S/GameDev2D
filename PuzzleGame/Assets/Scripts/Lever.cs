using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    private bool leverBool;
    public GameObject scratchingBoard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leverBool = !leverBool;
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Lvl 4")
        {
            if (leverBool)
            {
                scratchingBoard.SetActive(true);
            }
            else
            {
                scratchingBoard.SetActive(false);
            }
        }
    }
}
