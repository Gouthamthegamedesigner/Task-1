using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound; // Reference to the click sound
    [SerializeField] private float clickSoundVolume = 1f; // Volume for the click sound

    public void OnRetryButtonClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene("Level1");
    }

    public void OnHomeButtonClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene("HomeScreen");
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, clickSoundVolume);
        }
    }
}
