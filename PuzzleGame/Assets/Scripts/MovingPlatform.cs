using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int StartPoint;
    public float speed;
    public Transform[] points;
    private int i;

    private void Start()
    {
        transform.position = points[StartPoint].position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.1f)
        {
            i++;
            if (i >= points.Length)
                i = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
