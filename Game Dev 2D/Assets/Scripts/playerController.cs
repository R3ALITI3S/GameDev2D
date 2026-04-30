using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
    public LayerMask terrainLayer;
    public Rigidbody rb;
    private SpriteRenderer[] srs;
    public float groundCheckDistance = 2f;

    public Animator anim;
    private bool isAttacking = false;
    private float attackDelay = 1.4f;
    bool isGrounded;
    float x;
    float z;

    public static playerController Instance;
    [SerializeField] private GameObject cam;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.cam);
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackDelay(attackDelay));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");

            if (isGrounded)
            {
                anim.SetTrigger("Jump");
                Debug.Log("Jumping");
                // Apply jump force to the player
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, StatsManager.Instance.jumpForce, rb.linearVelocity.z);
            }
            else
            {
                Debug.Log("Not grounded");
            }
        }

        if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // animation (float-based)
        float SpeedWolf = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

        anim.SetFloat("Speed", SpeedWolf);

        // Debug.Log(SpeedWolf);
    }

    void FixedUpdate()
    {
        // Ground checking
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, terrainLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red); // draws a ray in the editor 


        // animation based on actual movement
        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

        // Movement
        rb.linearVelocity = new Vector3(x * StatsManager.Instance.speed, rb.linearVelocity.y, z * StatsManager.Instance.speed);
    }

    private IEnumerator AttackDelay(float delay)
    {
        isAttacking = true;
        anim.SetTrigger("Fight");
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }
}