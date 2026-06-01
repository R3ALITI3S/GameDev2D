using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public bool isPressed;
    int objectsOnButton = 0;
    public GameObject gate;
    public Sprite buttonUp;
    public Sprite buttonDown;
    public SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = buttonUp;
    }

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
        sr.sprite = buttonDown;
    }

    void Release()
    {
        isPressed = false;
        sr.sprite = buttonUp;
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

        if (SceneManager.GetActiveScene().name == "Lvl 5")
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
