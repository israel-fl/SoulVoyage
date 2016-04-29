using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public Text scoreText;

    public int currentScore = 0;
	
    void Start() {

        scoreText.text = "0";

    }

    public void IncrementScore() {
        currentScore += 10;
        scoreText.text = currentScore.ToString();
    }

    public int getScore() {
        return currentScore;
    }

}
