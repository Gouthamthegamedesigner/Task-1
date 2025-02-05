using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound; // Reference to the click sound
    [SerializeField] private float clickSoundVolume = 1f; // Volume for the click sound

    // Called when the Play button is clicked
    public void OnPlayButtonClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene("Level1");
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, clickSoundVolume);
        }
    }
}
