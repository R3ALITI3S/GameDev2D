using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool leverBool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leverBool = !leverBool;
            Debug.Log("Lever is now: " + leverBool);
        }
    }
}
