using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Rigidbody2D rb;
    public LineRenderer ropeLine;
    public LineRenderer trajectoryLine;
    public DistanceJoint2D dj;

    public LayerMask grappleLayer;

    public GameObject hookPrefab;
    public Transform firePoint;

    public float shootForce = 20f;
    public int trajectoryPoints = 30;
    public float trajectoryTimeStep = 0.1f;

    [Header("Player Reference")]
    public PlayerController player;

    private bool isGrappling;
    private Vector2 grapplePoint;
    private GameObject currentHook;

    void Start()
    {
        ropeLine.enabled = false;
        dj.enabled = false;

        if (player == null)
            player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DrawTrajectory();
        }
        else
        {
            trajectoryLine.enabled = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShootHook();
        }

        if (isGrappling)
        {
            ropeLine.SetPosition(0, transform.position);
            ropeLine.SetPosition(1, grapplePoint);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrappling)
        {
            StopGrapple();
        }
    }

    void ShootHook()
    {
        // Block shooting if the player does not have yarn equipped
        if (player != null && !player.IsYarnEquipped())
            return;

        if (currentHook != null)
        {
            Destroy(currentHook);
        }

        Vector2 mousePos = GetMouseWorldPosition();
        Vector2 direction = (mousePos - (Vector2)firePoint.position).normalized;

        currentHook = Instantiate(hookPrefab, firePoint.position, Quaternion.identity);

        GrappleHook gh = currentHook.GetComponent<GrappleHook>();
        gh.Launch(direction, shootForce, this, grappleLayer);

        // yarn cat 
        if (player != null)
        {
            player.SetCannotThrow();
        }
    }

    public void Attach(Vector2 point)
    {
        grapplePoint = point;
        isGrappling = true;

        ropeLine.enabled = true;
        dj.enabled = true;
        dj.connectedAnchor = point;

        // CAN'T THROW WHILE GRAPPLING!!
        if (player != null)
        {
            player.SetCannotThrow();
        }
    }

    void StopGrapple()
    {
        isGrappling = false;

        ropeLine.enabled = false;
        dj.enabled = false;

        if (currentHook != null)
        {
            Destroy(currentHook);
        }
    }

    void DrawTrajectory()
    {
        trajectoryLine.enabled = true;

        Vector2 startPos = firePoint.position;
        Vector2 mousePos = GetMouseWorldPosition();
        Vector2 velocity = (mousePos - startPos).normalized * shootForce;

        trajectoryLine.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector2 position = startPos + velocity * t + 0.5f * Physics2D.gravity * (t * t);
            trajectoryLine.SetPosition(i, position);
        }
    }

    Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}