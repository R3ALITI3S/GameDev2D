using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public bool isPressed;
    int objectsOnButton = 0;
    public GameObject gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            objectsOnButton++;
            if (!isPressed)
            {
                Press();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            objectsOnButton--;
            if (objectsOnButton <= 0)
            {
                Release();
            }
        }
    }

    void Press()
    {
        isPressed = true;
    }

    void Release()
    {
        isPressed = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lvl 4")
        {
            if (isPressed)
            {
                gate.SetActive(false);
            }
            else
            {
                gate.SetActive(true);
            }
        }
    }
}
