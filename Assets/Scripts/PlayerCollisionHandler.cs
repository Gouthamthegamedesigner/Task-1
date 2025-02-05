using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // Required for coroutines

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private int maxHitsFromAttacker1 = 3; // Max hits from Attacker1 before Game Over
    private int attacker1Hits = 0; // Tracks hits from Attacker1

    [SerializeField] private Text scoreText; // Reference to the UI Text for the score
    private int score = 0; // Tracks the player's score

    [SerializeField] private Text highScoreText; // Reference to the UI Text for the high score
    private int highScore = 0; // Tracks the highest score achieved

    [SerializeField] private AudioSource audioSource; // Reference to the Audio Source for the start sound
    [SerializeField] private AudioClip pointSoundEffect; // Sound effect for "Point" collision
    [SerializeField] private float pointSoundVolume = 1f; // Volume for point sound effect (default 1)

    [SerializeField] private AudioClip attackerSoundEffect; // Sound effect for Attacker collision
    [SerializeField] private float attackerSoundVolume = 1f; // Volume for Attacker collision sound

    private void Start()
    {
        // Load the high score from PlayerPrefs if it exists
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();

        // Play the audio when the game starts
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with Attacker1
        if (collision.gameObject.CompareTag("Attacker1"))
        {
            attacker1Hits++; // Increment hit count for Attacker1

            // Play the attacker sound effect with increased volume
            if (attackerSoundEffect != null)
            {
                PlayCollisionSound(attackerSoundEffect, attackerSoundVolume);
            }

            // Check if hits from Attacker1 reach the maximum
            if (attacker1Hits >= maxHitsFromAttacker1)
            {
                StartCoroutine(HandleGameOverWithDelay());
            }
        }
        // Check if collided with Attacker2
        else if (collision.gameObject.CompareTag("Attacker2"))
        {
            // Play the attacker sound effect with increased volume
            if (attackerSoundEffect != null)
            {
                PlayCollisionSound(attackerSoundEffect, attackerSoundVolume);
            }

            // Directly trigger Game Over for Attacker2 collision
            StartCoroutine(HandleGameOverWithDelay());
        }
        // Check if collided with Point
        else if (collision.gameObject.CompareTag("Point"))
        {
            // Increment the score and update the UI
            score++;
            UpdateScoreUI();

            // Check and update the high score in real-time
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore); // Save the high score immediately
                UpdateHighScoreUI(); // Update high score UI in real-time
            }

            // Play the point sound effect with increased volume
            if (pointSoundEffect != null)
            {
                PlayCollisionSound(pointSoundEffect, pointSoundVolume);
            }

            // Optionally, destroy the point object
            Destroy(collision.gameObject);
        }
    }

    private void PlayCollisionSound(AudioClip sound, float volume)
    {
        // Create a temporary AudioSource to play the sound with volume control
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = sound;
        tempAudioSource.volume = volume;
        tempAudioSource.Play();
    }

    private IEnumerator HandleGameOverWithDelay()
    {
        // Wait for the sound to finish playing (you can adjust the delay if needed)
        yield return new WaitForSeconds(1f); // Wait 1 second (adjust as needed)

        // Now load the "GameOver" scene
        LoadGameOverScene();
    }

    private void LoadGameOverScene()
    {
        // Asynchronously load the "GameOver" scene
        SceneManager.LoadSceneAsync("GameOver");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // Update the text in the UI
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString(); // Update the high score text in the UI
        }
    }
}
