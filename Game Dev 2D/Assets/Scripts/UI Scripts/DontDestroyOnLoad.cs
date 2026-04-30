using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{    
    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
