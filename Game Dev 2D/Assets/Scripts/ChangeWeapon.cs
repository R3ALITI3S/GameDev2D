using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeapon : MonoBehaviour
{
    // Assign a SpriteRenderer (e.g. your weapon display) in the Inspector
    public SpriteRenderer weaponRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Guard clauses
            if (weaponRenderer == null || weaponRenderer.sprite == null) return;

            var collidedRenderer = collision.GetComponent<SpriteRenderer>();
            if (collidedRenderer == null) return;

            // Swap sprites between the assigned SpriteRenderer and the collided object's SpriteRenderer
            Sprite pickedUpSprite = collidedRenderer.sprite;
            collidedRenderer.sprite = weaponRenderer.sprite;

            var ownRenderer = GetComponent<SpriteRenderer>();
            if (ownRenderer != null)
            {
                ownRenderer.sprite = pickedUpSprite;
            }
        }
    }
}
