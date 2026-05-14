using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    public EnemyStats enemyStats;

    [Header("Refs")]
    public Rigidbody2D rb;
    public Animator anim;

    private SpriteRenderer[] srs;

    private Vector2 moveInput;

    private bool isAttacking;
    private bool isDead;
    private bool canJump = true;

    public static PlayerController Instance;

    [SerializeField] private CinemachineCamera cam;

    void Awake()
    {
        // SINGLETON
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (cam != null)
            DontDestroyOnLoad(cam.gameObject);
    }

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (anim == null)
            anim = GetComponentInChildren<Animator>();

        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        // MOVEMENT INPUT
        moveInput = Vector2.zero;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput.x = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput.x = 1;

        // ONE JUMP ONLY
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canJump)
            TryJump();

        // ATTACK
        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryAttack();

        // FLIP PLAYER
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(moveInput.x),
                1,
                1
            );
        }

        // ANIMATION
        if (anim != null && rb != null)
            anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // DEATH CHECK
        if (StatsManager.Instance != null &&
            StatsManager.Instance.currentHealth <= 0)
        {
            PlayerDied();
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // MOVEMENT
        rb.linearVelocity = new Vector2(
            moveInput.x * StatsManager.Instance.speed,
            rb.linearVelocity.y
        );
    }

    void TryJump()
    {
        if (rb == null) return;

        canJump = false;

        if (anim != null)
            anim.SetTrigger("Jump");

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            StatsManager.Instance.jumpForce
        );
    }

    // JUMPS
    void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    void TryAttack()
    {
        if (!isAttacking)
            StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (anim != null)
            anim.SetTrigger("Fight");

        yield return new WaitForSeconds(1.2f);

        DealDamage(StatsManager.Instance.damage);

        isAttacking = false;
    }

    void DealDamage(int damage)
    {
        if (enemyStats == null) return;

        enemyStats.enemyCurrentHealth -=
            damage * StatsManager.Instance.level;
    }

    void PlayerDied()
    {
        if (isDead) return;

        isDead = true;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}