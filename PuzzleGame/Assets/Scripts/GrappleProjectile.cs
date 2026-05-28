using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Rigidbody2D rb;

    private Grapple playerGrapple;
    private LayerMask grappleLayer;
    private void Start()
    {
        Destroy(gameObject, 5f); //Change if you want the hook to last longer or shorter (Stays alive for 5 seconds if it doesn't hit anything)
    }

    public void Launch(Vector2 direction, float force, Grapple grapple, LayerMask layer)
    {
        playerGrapple = grapple;
        grappleLayer = layer;

        rb.linearVelocity = direction * force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & grappleLayer) != 0)
        {
            playerGrapple.Attach(transform.position);
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}