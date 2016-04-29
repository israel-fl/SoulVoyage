using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SeekerPlayGameManager : MonoBehaviour {

    public void GameOver() {

        Score score = GameManager.instance.score;

        PlayerPrefs.SetInt(PreferenceKeys.SeekerScoreKey, score.getScore());

        bool newHighScore = false;
        if (!PlayerPrefs.HasKey(PreferenceKeys.SeekerHighScoreKey) || PlayerPrefs.GetInt(PreferenceKeys.SeekerHighScoreKey) < score.getScore()) {
            PlayerPrefs.SetInt(PreferenceKeys.SeekerHighScoreKey, score.getScore());
            newHighScore = true;
        }
        PlayerPrefs.SetInt(PreferenceKeys.SeekerNewHighScoreKey, newHighScore ? 1 : 0);

        SceneManager.LoadScene(Scenes.SeekerGameOverScene);

    }

}
