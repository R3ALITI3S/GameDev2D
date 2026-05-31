using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [Header("Jump")]
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public float jumpForce;

    [Header("Refs")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    //public Animator anim;
    public GameObject yarn; // assigned when in range

    [Header("Pickup")]
    public Image uiPickupYarn;
    public float pickupRadius = 0.6f;           // radius around player to detect yarn
    public LayerMask yarnLayer;                 // set to include the Yarn layer

    private Vector2 moveInput;
    private bool isGrounded;
    private bool pickupYarn;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>();

        // Ensure UI starts hidden
        if (uiPickupYarn != null)
            uiPickupYarn.gameObject.SetActive(false);
    }

    void Update()
    {
        // INPUT
        moveInput = Vector2.zero;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput.x = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput.x = 1;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            TryJump();

        // Flip the sprite
        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);

        // Show/hide pickup UI (driven by detection code)
        if (uiPickupYarn != null)
            uiPickupYarn.gameObject.SetActive(pickupYarn);

        // Destroy yarn when in range and Enter pressed
        bool enterPressed = (Keyboard.current.enterKey != null && Keyboard.current.enterKey.wasPressedThisFrame);

        if (pickupYarn && enterPressed)
        {
            if (yarn != null)
            {
                Destroy(yarn);
                yarn = null;
            }
            pickupYarn = false;
            if (uiPickupYarn != null)
                uiPickupYarn.gameObject.SetActive(false);
        }

        //anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    void FixedUpdate()
    {
        if (groundCheck == null) return;

        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.down * 0.5f, groundCheckRadius, groundLayer

        // Proximity-based detection around the player (robust and doesn't require triggers)
        Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRadius, yarnLayer);
        if (hit != null && hit.CompareTag("Yarn"))
        {
            pickupYarn = true;
            yarn = hit.gameObject;
        }
        else
        {
            pickupYarn = false;
            // keep yarn null unless in-range
            yarn = null;
        }

        rb.linearVelocity = new Vector2(
            moveInput.x * speed,
            rb.linearVelocity.y
        );
    }

    void TryJump()
    {
        if (!isGrounded) return;

        //anim.SetTrigger("Jump");

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    // Optional: visualize the pickup radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}