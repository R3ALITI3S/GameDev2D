using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

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
    public Animator anim;
    public GameObject yarn; // assigned when in range

    [Header("Sprite Libraries")]
    public SpriteLibrary spriteLibrary;

    public SpriteLibraryAsset normalCat;
    public SpriteLibraryAsset catWithYarnCanThrow;
    public SpriteLibraryAsset catWithYarnCannotThrow;

    [Header("Pickup")]
    public Image uiPickupYarn;
    public float pickupRadius = 0.6f;           // radius around player to detect yarn
    public LayerMask yarnLayer;                 // set to include the Yarn layer

    private Vector2 moveInput;
    private bool isGrounded;
    private bool pickupYarn;
    private Vector3 originalScale;

    public static PlayerController Instance;

    // 3 gameplay states
    private enum CatState
    {
        Normal,
        YarnCanThrow,
        YarnCannotThrow
    }

    private CatState currentState = CatState.Normal;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        originalScale = transform.localScale;

        if (spriteLibrary == null)
            spriteLibrary = GetComponentInChildren<SpriteLibrary>();

        // Ensure UI starts hidden
        if (uiPickupYarn != null)
            uiPickupYarn.gameObject.SetActive(false);

        UpdateVisualState();
    }

    void Update()
    {
        // INPUT & ANIMATION DRIVEN TOGETHER AT THE KEYBOARD PRESS
        moveInput = Vector2.zero;

        // Check if any movement keys are actively held down this frame
        bool leftPressed = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed;
        bool rightPressed = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed;

        if (leftPressed)
        {
            moveInput.x = -1;
            if (anim != null) anim.SetBool("isWalking", true);
        }
        else if (rightPressed)
        {
            moveInput.x = 1;
            if (anim != null) anim.SetBool("isWalking", true);
        }
        else
        {
            // No keys pressed → turn off animation instantly
            if (anim != null) anim.SetBool("isWalking", false);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            TryJump();

        // Flip the sprite (keeps original scale safe)
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(moveInput.x) * Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        // Show/hide pickup UI (driven by detection code)
        if (uiPickupYarn != null)
            uiPickupYarn.gameObject.SetActive(pickupYarn);

        // Pick up yarn
        bool enterPressed = (Keyboard.current.enterKey != null && Keyboard.current.enterKey.wasPressedThisFrame);

        if (pickupYarn && enterPressed)
        {
            if (yarn != null)
            {
                Destroy(yarn);
                yarn = null;

                // When yarn is picked up → can throw
                SetCatState(CatState.YarnCanThrow);
            }

            pickupYarn = false;

            if (uiPickupYarn != null)
                uiPickupYarn.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (groundCheck == null) return;

        isGrounded = Physics2D.OverlapCircle(
            (Vector2)transform.position + Vector2.down * 0.5f,
            groundCheckRadius,
            groundLayer
        );

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

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    // MAIN STATE SWITCH
    void SetCatState(CatState newState)
    {
        currentState = newState;
        UpdateVisualState();
    }

    // sprite library asset - so wonky
    void UpdateVisualState()
    {
        if (spriteLibrary == null) return;

        switch (currentState)
        {
            case CatState.Normal:
                spriteLibrary.spriteLibraryAsset = normalCat;
                break;

            case CatState.YarnCanThrow:
                spriteLibrary.spriteLibraryAsset = catWithYarnCanThrow;
                break;

            case CatState.YarnCannotThrow:
                spriteLibrary.spriteLibraryAsset = catWithYarnCannotThrow;
                break;
        }
    }

    // Call this if something prevents throwing!
    public void SetCannotThrow()
    {
        if (currentState == CatState.YarnCanThrow)
            SetCatState(CatState.YarnCannotThrow);
    }

    // Call this when yarn is thrown or dropped!
    public void ReturnToNormal()
    {
        SetCatState(CatState.Normal);
    }

    // Optional: visualize the pickup radius in editor!
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