using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;       // How fast the boss moves
    public float stoppingDistance = 1.5f; // How close the boss gets before stopping

    [Header("Stuck Settings")]
    public float stuckDuration = 1.5f; // How long the boss stays stuck

    private Vector3 originalScale;
    private Animator anim;
    private bool isStuck = false; // Tracks movement state internally

    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        // Save original scale
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 1. Don't do anything if there is no player, or if the boss is stuck
        if (player == null || isStuck) return;

        // 2. Always face the player while chasing
        LookAtPlayer();

        // 3. Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 4. Move towards the player if outside the stopping distance
        if (distanceToPlayer > stoppingDistance)
        {
            // Move only on the X-axis (common for 2D platformers/side-scrollers)
            // If you want 8-directional top-down movement, change player.position.x to player.position
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Optional: Tell animator we are moving
            if (anim != null) anim.SetBool("IsMoving", true);
        }
        else
        {
            // Optional: Tell animator we stopped
            if (anim != null) anim.SetBool("IsMoving", false);
        }
    }

    /// <summary>
    /// Starts the countdown to set HammerStuck back to false.
    /// Called directly by the boss_run StateMachineBehaviour.
    /// </summary>
    public void StartUnstuckTimer()
    {
        isStuck = true; // Stop movement
        if (anim != null) anim.SetBool("IsMoving", false);

        CancelInvoke(nameof(ResetStuck));
        Invoke(nameof(ResetStuck), stuckDuration);
    }

    private void ResetStuck()
    {
        isStuck = false; // Resume movement
        if (anim != null)
        {
            anim.SetBool("HammerStuck", false);
        }
    }

    public void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 scale = originalScale;

        // Flip LEFT
        if (player.position.x < transform.position.x)
        {
            scale.x = -Mathf.Abs(originalScale.x);
        }
        // Flip RIGHT
        else
        {
            scale.x = Mathf.Abs(originalScale.x);
        }

        transform.localScale = scale;
    }
}