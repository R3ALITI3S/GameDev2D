using UnityEngine;

public class Buttons : MonoBehaviour
{
    public bool isPressed;
    int objectsOnButton = 0;

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
}
