using System.Collections.Generic;
using UnityEngine;

public class YarnRope2D : MonoBehaviour
{
    [Header("References")]
    public Transform anchorPoint;
    public Rigidbody2D catBody;
    public GameObject ropeSegmentPrefab;

    [Header("Rope Settings")]
    public int segmentCount = 12;
    public float segmentLength = 0.25f;
    public float ropeGravityScale = 1f;

    [Header("Physics Tuning")]
    public float linearDrag = 0.6f;
    public float angularDrag = 1.2f;

    [Header("Spring Settings (First Joint)")]
    public float springFrequency = 6f;
    public float springDamping = 0.7f;

    [Header("Line Renderer")]
    public LineRenderer line;

    private List<Rigidbody2D> segments = new List<Rigidbody2D>();

    void Start()
    {
        BuildRope();
    }

    void BuildRope()
    {
        segments.Clear();

        Rigidbody2D previous = anchorPoint.GetComponent<Rigidbody2D>();

        Vector2 startPos = anchorPoint.position;

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject seg = Instantiate(ropeSegmentPrefab);

            // Proper spacing.........
            seg.transform.position = startPos + Vector2.down * segmentLength * (i + 1);

            Rigidbody2D rb = seg.GetComponent<Rigidbody2D>();

            rb.gravityScale = ropeGravityScale;
            rb.linearDamping = linearDrag;
            rb.angularDamping = angularDrag;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            segments.Add(rb);

            // soft first spring
            if (i == 0)
            {
                SpringJoint2D spring = seg.AddComponent<SpringJoint2D>();
                spring.autoConfigureDistance = false;
                spring.distance = segmentLength;

                spring.frequency = springFrequency;
                spring.dampingRatio = springDamping;

                spring.enableCollision = true;
                spring.connectedBody = previous;
            }
            else
            {
                DistanceJoint2D joint = seg.AddComponent<DistanceJoint2D>();
                joint.autoConfigureDistance = false;
                joint.distance = segmentLength;
                joint.maxDistanceOnly = false;
                joint.enableCollision = true;
                joint.connectedBody = previous;
            }

            previous = rb;
        }

        // Attach cat 
        SpringJoint2D catJoint = catBody.gameObject.AddComponent<SpringJoint2D>();
        catJoint.autoConfigureDistance = false;
        catJoint.distance = segmentLength;

        catJoint.frequency = springFrequency;
        catJoint.dampingRatio = springDamping;

        catJoint.enableCollision = true;
        catJoint.connectedBody = previous;
    }

    void LateUpdate()
    {
        if (line == null || segments.Count == 0) return;

        line.positionCount = segments.Count + 1;

        line.SetPosition(0, anchorPoint.position);

        for (int i = 0; i < segments.Count; i++)
        {
            line.SetPosition(i + 1, segments[i].position);
        }
    }
}