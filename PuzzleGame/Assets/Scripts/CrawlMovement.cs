using UnityEngine;
using UnityEngine.InputSystem;

public class CrawlMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;

    [Header("Movement")]
    public float crawlSpeed = 5f;
    public float jumpForce = 10f;
    public float wallJumpX = 5f;

    Vector2 wallNormal;
    bool crawling;

    PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();

        enabled = false;
    }

    void OnEnable()
    {
        crawling = true;

        if (player != null)
            player.enabled = false;   // ❗ STOP CONFLICT

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    void OnDisable()
    {
        crawling = false;

        if (player != null)
            player.enabled = true;    // ❗ RETURN CONTROL

        rb.gravityScale = 1f;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (!crawling) return;

        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            moveInput.y = 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            moveInput.y = -1;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            TryJump();
    }

    void FixedUpdate()
    {
        if (!crawling) return;

        rb.linearVelocity = new Vector2(0f, moveInput.y * crawlSpeed);

        LockRotation();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        wallNormal = collision.GetContact(0).normal;
    }

    void LockRotation()
    {
        // FIXED BOTH WALLS
        float z = wallNormal.x > 0f ? -90f : 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }

    void TryJump()
    {
        crawling = false;

        rb.gravityScale = 1f;

        float dir = -wallNormal.x;

        rb.linearVelocity = new Vector2(dir * wallJumpX, jumpForce);

        enabled = false;
    }
}