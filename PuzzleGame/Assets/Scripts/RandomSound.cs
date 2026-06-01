using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip[] normalSounds;
    public AudioClip specialSound;

    [Header("Chance Settings")]
    [Range(0f, 1f)]
    public float specialChance = 0.1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (Random.value < specialChance && specialSound != null)
        {
            audioSource.PlayOneShot(specialSound);
        }
        else
        {
            if (normalSounds.Length == 0) return;

            AudioClip clip = normalSounds[Random.Range(0, normalSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}