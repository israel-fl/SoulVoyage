using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FreePlayGameOver : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	void Start () {
		scoreText.text = SecondsToString(PlayerPrefs.GetFloat(PreferenceKeys.FreePlayScoreKey));
		highScoreText.text = "High Score: " + SecondsToString(PlayerPrefs.GetFloat(PreferenceKeys.FreePlayHighScoreKey));
	}
	
	private string SecondsToString(float time) {
		int minutes = Mathf.FloorToInt(time / 60);
		int seconds = Mathf.FloorToInt(time - 60f * minutes);
		return minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
	}

	public void PlayAgain() {
		SceneManager.LoadScene(Scenes.FreePlayScene);
	}

    public void GoToMenu() {
        SceneManager.LoadScene(Scenes.MainMenuScene);
    }

}
