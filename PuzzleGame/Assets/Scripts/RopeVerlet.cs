using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class RopeVerlet : MonoBehaviour
{
    [Header("Rope")]
    [SerializeField] private int _numOfRopeSegments = 50;
    [SerializeField] private float _ropeSegmentLength = 0.225f;

    [Header("Physics")]
    [SerializeField] private Vector2 _gravityForce = new Vector2(0f, -9.81f);
    [SerializeField] private float _dampingFactor = 0.98f;

    [Header("Collision")]
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _collisionRadius = 0.25f;
    [SerializeField] private float _bounceFactor = 0.0f;

    [Header("Constraints")]
    [SerializeField] private int _numOfConstraintRuns = 50;

    [Header("Optimization")]
    [SerializeField] private int _collisionSegmentInterval = 2;

    private LineRenderer _lineRenderer;
    private readonly List<RopeSegment> _ropeSegments = new();

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _numOfRopeSegments;

        Vector2 startPoint = transform.position;

        _ropeSegments.Clear();

        for (int i = 0; i < _numOfRopeSegments; i++)
        {
            _ropeSegments.Add(new RopeSegment(startPoint));

            startPoint.y -= _ropeSegmentLength;
        }
    }

    private void Update()
    {
        DrawRope();
    }

    private void FixedUpdate()
    {
        Simulate();

        for (int i = 0; i < _numOfConstraintRuns; i++)
        {
            // Collision BEFORE constraints works better
            if (i % _collisionSegmentInterval == 0)
            {
                HandleCollisions();
            }

            ApplyConstraints();
        }
    }

    private void Simulate()
    {
        // Skip first segment because it is pinned to mouse
        for (int i = 1; i < _ropeSegments.Count; i++)
        {
            RopeSegment segment = _ropeSegments[i];

            Vector2 velocity =
                (segment.CurrentPosition - segment.OldPosition) * _dampingFactor;

            segment.OldPosition = segment.CurrentPosition;

            segment.CurrentPosition += velocity;
            segment.CurrentPosition +=
                _gravityForce * Time.fixedDeltaTime;

            _ropeSegments[i] = segment;
        }
    }

    private void ApplyConstraints()
    {
        // Pin first segment to mouse
        RopeSegment firstSegment = _ropeSegments[0];

        Vector3 mouseWorld =
            _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        firstSegment.CurrentPosition =
            new Vector2(mouseWorld.x, mouseWorld.y);

        _ropeSegments[0] = firstSegment;

        // Distance constraints
        for (int i = 0; i < _ropeSegments.Count - 1; i++)
        {
            RopeSegment currentSeg = _ropeSegments[i];
            RopeSegment nextSeg = _ropeSegments[i + 1];

            Vector2 delta =
                nextSeg.CurrentPosition - currentSeg.CurrentPosition;

            float distance = delta.magnitude;

            float error = distance - _ropeSegmentLength;

            if (distance > 0.0001f)
            {
                Vector2 changeDir = delta / distance;

                Vector2 changeAmount = changeDir * error;

                if (i == 0)
                {
                    // first segment pinned
                    nextSeg.CurrentPosition -= changeAmount;
                }
                else
                {
                    currentSeg.CurrentPosition += changeAmount * 0.5f;
                    nextSeg.CurrentPosition -= changeAmount * 0.5f;
                }

                _ropeSegments[i] = currentSeg;
                _ropeSegments[i + 1] = nextSeg;
            }
        }
    }

    private void HandleCollisions()
    {
        for (int i = 1; i < _ropeSegments.Count; i++)
        {
            RopeSegment segment = _ropeSegments[i];

            Vector2 velocity =
                segment.CurrentPosition - segment.OldPosition;

            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(
                    segment.CurrentPosition,
                    _collisionRadius,
                    _collisionMask
                );

            foreach (Collider2D collider in colliders)
            {
                Vector2 closestPoint =
                    collider.ClosestPoint(segment.CurrentPosition);

                Vector2 collisionVector =
                    segment.CurrentPosition - closestPoint;

                float distance = collisionVector.magnitude;

                // Prevent divide by zero
                if (distance == 0f)
                {
                    collisionVector =
                        (segment.CurrentPosition -
                         (Vector2)collider.transform.position).normalized;

                    distance = 0.0001f;
                }

                // Resolve overlap
                if (distance < _collisionRadius)
                {
                    Vector2 normal =
                        collisionVector.normalized;

                    float penetration =
                        _collisionRadius - distance;

                    segment.CurrentPosition +=
                        normal * penetration;

                    // Optional bounce
                    velocity =
                        Vector2.Reflect(velocity, normal) *
                        _bounceFactor;
                }
            }

            segment.OldPosition =
                segment.CurrentPosition - velocity;

            _ropeSegments[i] = segment;
        }
    }

    private void DrawRope()
    {
        Vector3[] positions =
            new Vector3[_ropeSegments.Count];

        for (int i = 0; i < _ropeSegments.Count; i++)
        {
            positions[i] = _ropeSegments[i].CurrentPosition;
        }

        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }

    // Debug collision circles
    private void OnDrawGizmos()
    {
        if (_ropeSegments == null)
            return;

        Gizmos.color = Color.red;

        foreach (RopeSegment segment in _ropeSegments)
        {
            Gizmos.DrawWireSphere(
                segment.CurrentPosition,
                _collisionRadius
            );
        }
    }

    public struct RopeSegment
    {
        public Vector2 CurrentPosition;
        public Vector2 OldPosition;

        public RopeSegment(Vector2 position)
        {
            CurrentPosition = position;
            OldPosition = position;
        }
    }
}