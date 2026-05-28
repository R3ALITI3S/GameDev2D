using UnityEngine;
using UnityEngine.InputSystem;

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

    private Vector2 moveInput;
    private bool isGrounded;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>();
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


        //anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

    }

    void FixedUpdate()
    {
        if (groundCheck == null) return;


        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.down * 0.5f, groundCheckRadius, groundLayer
);


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

}