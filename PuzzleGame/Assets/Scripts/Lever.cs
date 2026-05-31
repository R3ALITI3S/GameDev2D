using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    private bool leverBool;
    public GameObject scratchingBoard;
    public Transform pivotRotation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leverBool = !leverBool;

            float angle = leverBool ? 30f : -30f;
            pivotRotation.rotation = Quaternion.Euler(0f, 0f, angle);
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
