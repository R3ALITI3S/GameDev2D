using UnityEngine;

[System.Serializable]
public class ParallaxLayerForeground
{
    public Transform tile;
    [Range(0f, 1f)] public float parallaxStrength = 0.5f;

    [HideInInspector] public Vector3 startPos;
}

public class Foreground : MonoBehaviour
{
    [SerializeField] private ParallaxLayerForeground[] layers;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        // Cache original positions
        foreach (var layer in layers)
        {
            if (layer.tile != null)
                layer.startPos = layer.tile.position;
        }
    }

    private void LateUpdate()
    {
        Vector3 camPos = cameraTransform.position;

        foreach (var layer in layers)
        {
            if (layer.tile == null) continue;

            layer.tile.position = new Vector3(
                layer.startPos.x + camPos.x * layer.parallaxStrength,
                layer.startPos.y + camPos.y * layer.parallaxStrength,
                layer.startPos.z
            );
        }
    }
}