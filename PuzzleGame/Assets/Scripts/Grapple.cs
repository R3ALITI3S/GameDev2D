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

    private bool isGrappling;
    private Vector2 grapplePoint;
    private GameObject currentHook;

    void Start()
    {
        ropeLine.enabled = false;
        trajectoryLine.enabled = false;
        dj.enabled = false;
    }

    void Update()
    {
        if (PlayerController.Instance == null)
            return;

        bool canShoot =
            PlayerController.Instance.yarnState == YarnState.YarnBall;

        if (canShoot)
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
        }
        else
        {
            trajectoryLine.enabled = false;
        }

        if (isGrappling)
        {
            ropeLine.SetPosition(0, transform.position);
            ropeLine.SetPosition(1, grapplePoint);
        }

        if (Input.GetMouseButtonDown(1) && isGrappling)
        {
            StopGrapple();
        }
    }

    void ShootHook()
    {
        if (PlayerController.Instance == null)
            return;

        if (PlayerController.Instance.yarnState != YarnState.YarnBall)
            return;

        if (currentHook != null)
        {
            Destroy(currentHook);
            currentHook = null;
        }

        PlayerController.Instance.SetYarnState(YarnState.YarnStomach);

        Vector2 mousePos = GetMouseWorldPosition();
        Vector2 direction =
            (mousePos - (Vector2)firePoint.position).normalized;

        currentHook = Instantiate(
            hookPrefab,
            firePoint.position,
            Quaternion.identity
        );

        GrappleHook gh = currentHook.GetComponent<GrappleHook>();
        gh.Launch(direction, shootForce, this, grappleLayer);
    }

    public void Attach(Vector2 point)
    {
        grapplePoint = point;
        isGrappling = true;

        ropeLine.enabled = true;
        dj.enabled = true;

        dj.connectedAnchor = point;
    }

    void StopGrapple()
    {
        isGrappling = false;

        ropeLine.enabled = false;
        dj.enabled = false;

        if (currentHook != null)
        {
            Destroy(currentHook);
            currentHook = null;
        }
    }

    void DrawTrajectory()
    {
        trajectoryLine.enabled = true;

        Vector2 startPos = firePoint.position;
        Vector2 mousePos = GetMouseWorldPosition();

        Vector2 velocity =
            (mousePos - startPos).normalized * shootForce;

        trajectoryLine.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * trajectoryTimeStep;

            Vector2 position =
                startPos +
                velocity * t +
                0.5f * Physics2D.gravity * (t * t);

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