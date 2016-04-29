using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SeekerPlayGameOver : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;

    void Start() {
        scoreText.text = PlayerPrefs.GetInt(PreferenceKeys.SeekerScoreKey).ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(PreferenceKeys.SeekerHighScoreKey).ToString();
    }

    public void PlayAgain() {
        SceneManager.LoadScene(Scenes.SeekerScene);
    }

    public void GoToMenu() {
        SceneManager.LoadScene(Scenes.MainMenuScene);
    }

}
