using UnityEngine;
using UnityEngine.InputSystem;

public class CrawlMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    public float jumpForce;
    public float crawlSpeed;
    public float wallJumpX = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }


    void Update()
    {
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
        rb.linearVelocity = new Vector2(0, moveInput.y * crawlSpeed);
    }

    void TryJump()
    {
        rb.gravityScale = 1f;
        float direction = transform.localScale.x;
        rb.linearVelocity = new Vector2(-direction * wallJumpX, jumpForce);
        enabled = false;
    }
}
