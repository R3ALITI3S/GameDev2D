using UnityEngine;

public class ScratchingBoard : MonoBehaviour
{
    Collider2D wallCollider;

    private void Start()
    {
        wallCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerMovement = collision.gameObject.GetComponent<PlayerController>();
            CrawlMovement crawlMovement = collision.gameObject.GetComponent<CrawlMovement>();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerMovement.enabled = false;
            crawlMovement.enabled = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.gameObject.GetComponent<Collider2D>();
            if (playerCollider.bounds.min.y > wallCollider.bounds.max.y)
            {
                DetachPlayer(collision.gameObject);
            }
        }
    }

    void DetachPlayer(GameObject player)
    {
        PlayerController playerMovement = player.GetComponent<PlayerController>();
        CrawlMovement crawlMovement = player.GetComponent<CrawlMovement>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        playerMovement.enabled = true;
        crawlMovement.enabled = false;
        rb.gravityScale = 1f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerMovement = collision.gameObject.GetComponent<PlayerController>();
            CrawlMovement crawlMovement = collision.gameObject.GetComponent<CrawlMovement>();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerMovement.enabled = true;
            crawlMovement.enabled = false;
            rb.gravityScale = 1f;
        }
    }
}