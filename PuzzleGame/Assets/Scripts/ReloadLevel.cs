using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fader.gameObject.SetActive(true);
            LeanTween.scale(fader, Vector3.zero, 0f);
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                Invoke("LevelReloading", 0.5f);
            });
        }
    }

    private void LevelReloading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
