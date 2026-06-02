using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    private bool leverBool;
    public GameObject obj;
    public Transform pivotRotation;
    public MovingPlatform movingPlatform;

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
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Lvl 5")
        {
            if (leverBool)
            {
                movingPlatform.enabled = true;
            }
            else
            {
                movingPlatform.enabled = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "Lvl 6")
        {
            if (leverBool)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Lvl 11")
        {
            if (leverBool)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
