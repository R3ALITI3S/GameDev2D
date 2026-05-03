using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform[] tiles;              // assign 2–3 copies of the same sprite
    [Range(0f, 1f)] public float parallaxStrength = 0.5f;

    [HideInInspector] public float spriteWidth;
}

public class Background : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] layers;
    [SerializeField] private Transform cameraTransform;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;

        // Calculate sprite widths
        foreach (var layer in layers)
        {
            if (layer.tiles.Length == 0) continue;

            var sr = layer.tiles[0].GetComponent<SpriteRenderer>();
            if (sr != null)
                layer.spriteWidth = sr.bounds.size.x;
        }
    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;

        foreach (var layer in layers)
        {
            if (layer.tiles.Length == 0) continue;

            float moveX = delta.x * layer.parallaxStrength;

            foreach (var tile in layer.tiles)
            {
                tile.position += new Vector3(moveX, 0f, 0f);
            }

            // Loop tiles
            Transform leftMost = layer.tiles[0];
            Transform rightMost = layer.tiles[0];

            foreach (var t in layer.tiles)
            {
                if (t.position.x < leftMost.position.x) leftMost = t;
                if (t.position.x > rightMost.position.x) rightMost = t;
            }

            float camX = cameraTransform.position.x;

            // If left tile is too far left → move it to the right
            if (camX - leftMost.position.x > layer.spriteWidth)
            {
                leftMost.position = new Vector3(
                    rightMost.position.x + layer.spriteWidth,
                    leftMost.position.y,
                    leftMost.position.z
                );
            }

            // If right tile is too far right → move it to the left
            if (rightMost.position.x - camX > layer.spriteWidth)
            {
                rightMost.position = new Vector3(
                    leftMost.position.x - layer.spriteWidth,
                    rightMost.position.y,
                    rightMost.position.z
                );
            }
        }

        lastCameraPosition = cameraTransform.position;
    }
}