using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FreePlayGameManager : MonoBehaviour {

	public Timer timer;

	public void GameOver() {
		PlayerPrefs.SetFloat(PreferenceKeys.FreePlayScoreKey, timer.Value);

		bool newHighScore = false;
		if (!PlayerPrefs.HasKey(PreferenceKeys.FreePlayHighScoreKey) || PlayerPrefs.GetFloat(PreferenceKeys.FreePlayHighScoreKey) < timer.Value) {
			PlayerPrefs.SetFloat(PreferenceKeys.FreePlayHighScoreKey, timer.Value);
			newHighScore = true;
		}
		PlayerPrefs.SetInt(PreferenceKeys.FreePlayNewHighScoreKey, newHighScore ? 1 : 0);

		SceneManager.LoadScene(Scenes.FreePlayGameOverScene);
	}

}
