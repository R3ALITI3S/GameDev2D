using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;
    public AudioSource audioSource;

    void Awake()
    {
        // Prevent duplicates when changing scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}