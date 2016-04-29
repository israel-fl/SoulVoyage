using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public Slider backgroundMusicVolumeSlider;

	void Start() {
		backgroundMusicVolumeSlider.value = GameManager.instance.backgroundMusicSource.volume;
	}
	
	public void GoToMainMenu() {
		GameManager.instance.playManager.Play();
		SceneManager.LoadScene(Scenes.MainMenuScene);
	}

	public void RestartLevel() {
		GameManager.instance.playManager.Play();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
