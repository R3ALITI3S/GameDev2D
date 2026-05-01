using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public EnemyStats enemyStats;

    [Header("Jump")]
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    [Header("Refs")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator anim;

    private Vector2 moveInput;
    private bool isGrounded;
    private bool isAttacking;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
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

        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryAttack();

        // Flip the sprite
        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);

        
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        if (StatsManager.Instance.currentHealth <= 0)
        {
                playerDied();
        }
    }

    void FixedUpdate()
    {
        if (groundCheck == null) return;

        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        rb.linearVelocity = new Vector2(
            moveInput.x * StatsManager.Instance.speed,
            rb.linearVelocity.y
        );
    }

    void TryJump()
    {
        if (!isGrounded) return;

        anim.SetTrigger("Jump");

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, StatsManager.Instance.jumpForce);
    }

    void TryAttack()
    {
        if (!isAttacking)
            StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Fight");

        yield return new WaitForSeconds(1.2f);
        DealDamage(StatsManager.Instance.damage);
        isAttacking = false;
    }

    void DealDamage(int damage)
    {
        enemyStats.enemyCurrentHealth -= damage * StatsManager.Instance.level;
    }

    void playerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}