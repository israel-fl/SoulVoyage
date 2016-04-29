using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * PlayManager
 * 
 * Handles play/pause functionality
 * 
 */

public class PlayManager : MonoBehaviour {
	
	public Button pauseButton;
	public GameObject pauseMenu;

	public bool isPaused { get; private set; }

	private float prePauseTimeScale = 1f;

	void Awake() {
		isPaused = false;

		pauseMenu.SetActive(false);
		pauseButton.enabled = true;
	}

	public void Play() {
		if (isPaused) {
			// Update pause state
			isPaused = false;

			// Unpause game physics
			Time.timeScale = prePauseTimeScale;

			// Update UI
			pauseMenu.SetActive(false);
			pauseButton.enabled = true;

			// Unpause game audio
			AudioListener.pause = false;
		}
	}

	public void Pause() {
		// Check that game can be paused
		if (!isPaused) {
			// Update pause state
			isPaused = true;

			// Pause game physics, store current settings
			prePauseTimeScale = Time.timeScale;
			Time.timeScale = 0f;

			// Update UI
			pauseMenu.SetActive(true);
			pauseButton.enabled = false;

			// Pause game audio
			AudioListener.pause = true;
		}
	}

	void OnApplicationPause(bool shouldPause) {
		// Pause the game when the app becomes inactive
		if (shouldPause) {
			Pause();
		}
	}
}
