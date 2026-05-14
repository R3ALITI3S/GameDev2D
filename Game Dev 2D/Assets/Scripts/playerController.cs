using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

public class PlayerController : MonoBehaviour
{
    public EnemyStats enemyStats;
    public StatsUpgrade statsUpgrade;

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

    private float currentHealth;

    private float healthUpgrade = 1f;
    private float damageUpgrade = 1f;
    private float defenseUpgrade = 1f;
    private float speedUpgrade = 1f;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (statsUpgrade != null)
        {
            statsUpgrade.upgradeHealthAmount = healthUpgrade;
            statsUpgrade.upgradeDamageAmount = damageUpgrade;
            statsUpgrade.upgradeDefenseAmount = defenseUpgrade;
            statsUpgrade.upgradeSpeedAmount = speedUpgrade;
        }

        // Initialize current health from StatsManager maxHealth (guarding null)
        if (StatsManager.Instance != null)
        {
            if (currentHealth <= 0f)
                currentHealth = StatsManager.Instance.maxHealth;

            StatsManager.Instance.currentHealth = currentHealth;
        }
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

        if (currentHealth <= 0)
        {
                playerDied();
        }
    }

    void FixedUpdate()
    {
        if (groundCheck == null) return;

        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        rb.linearVelocity = new Vector2(
            moveInput.x * StatsManager.Instance.speed * (speedUpgrade * 0.5f),
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

    void DealDamage(float damage)
    {
        enemyStats.enemyCurrentHealth -= damage * damageUpgrade;
    }

    void playerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}