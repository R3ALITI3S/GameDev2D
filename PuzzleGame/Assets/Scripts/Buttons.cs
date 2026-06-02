using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public bool isPressed;
    int objectsOnButton = 0;
    public Transform pivotRotationLvl3;
    public GameObject gate;
    public Sprite buttonUp;
    public Sprite buttonDown;
    public SpriteRenderer sr;
    public MovingPlatform movingPlatform;
    public GameObject obstacleLvl11;

    // Smooth rotation settings
    public float rotationSpeed = 180f; // degrees per second
    public float pressedZ = 90f;
    public float defaultZ = 0f;

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
        if (SceneManager.GetActiveScene().name == "Lvl 3")
        {
            if (pivotRotationLvl3 != null)
            {
                Quaternion target = isPressed
                    ? Quaternion.Euler(0f, 0f, pressedZ)
                    : Quaternion.Euler(0f, 0f, defaultZ);

                // Smoothly rotate toward target over time
                pivotRotationLvl3.rotation = Quaternion.RotateTowards(
                    pivotRotationLvl3.rotation,
                    target,
                    rotationSpeed * Time.deltaTime);
            }
        }

        if (SceneManager.GetActiveScene().name == "Lvl 4")
        {
            if (isPressed)
            {
                movingPlatform.enabled = true;
            }
            else
            {
                movingPlatform.enabled = false;
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

        if (SceneManager.GetActiveScene().name == "Lvl 6")
        {
            if (isPressed)
            {
                gate.SetActive(true);
            }
            else
            {
                gate.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Lvl 11" && gate != null)
        {
            if (isPressed && gate != null)
            {
                gate.SetActive(false);
            }
            else
            {
                gate.SetActive(true);
            }
        }
        if (SceneManager.GetActiveScene().name == "Lvl 11" && obstacleLvl11 != null)
        {
            if (isPressed && obstacleLvl11 != null)
            {
                obstacleLvl11.SetActive(false);
            }
            else
            {
                obstacleLvl11.SetActive(true);
            }
        }
    }
}
