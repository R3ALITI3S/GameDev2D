using UnityEngine;

public class BridgeShake : MonoBehaviour
{
    public CameraShake cameraShake;

    public void ImpactShake()
    {
        cameraShake.Shake();
    }
}