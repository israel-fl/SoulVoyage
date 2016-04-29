using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerText;

	public bool showUnits = false;

	public bool showHours = false;
	public bool showMinutes = true;
	public bool showSeconds = true;

	private bool isRunning;
	private float measureStartTime;
	private float savedTime;

	public enum StartMode {
		MANUAL,
		ON_GAME_START,
		ON_FIRST_UPDATE
	}

	public StartMode startMode;

	void Start () {
		timerText.text = "0:00";

		isRunning = false;
		measureStartTime = 0;
		savedTime = 0;

		if (startMode == StartMode.ON_GAME_START) {
			StartTimer();
		}
	}

	private bool firstUpdateLoop = true;

	void Update () {
		if (startMode == StartMode.ON_FIRST_UPDATE && firstUpdateLoop) {
			StartTimer();
			firstUpdateLoop = false;
		}

		// Only compute timer text if necessary
		if (timerText == null) {
			return;
		}

		string text = "";
		float time = savedTime + (isRunning ? (Time.time - measureStartTime) : 0f);

		if (showHours) {
			int hours = Mathf.FloorToInt(time / 360);
			time -= 360f * hours;

			text += hours;

			if (showUnits) {
				text += " hr" + (showMinutes ? " " : "");
			} else if (showMinutes) {
				text += ":";
			}
		}

		if (showMinutes) {
			int minutes = Mathf.FloorToInt(time / 60);
			time -= 60f * minutes;

			if (!showUnits && showHours && minutes < 10) {
				text += "0";
			}
			text += minutes;

			if (showUnits) {
				text += " min" + (showSeconds ? " " : "");
			} else if (showSeconds) {
				text += ":";
			}
		}

		if (showSeconds) {
			int seconds = Mathf.FloorToInt(time);

			if (!showUnits && showMinutes && seconds < 10) {
				text += "0";
			}
			text += seconds;

			if (showUnits) {
				text += " sec";
			}
		}

		timerText.text = text;
	}

	/*
	 * Value
	 * 
	 * Current value of the timer (in seconds).
	 * 
	 */
	public float Value {
		get {
			return savedTime + (isRunning ? (Time.time - measureStartTime) : 0f);
		}
	}


	/*
	 * StartTimer()
	 * 
	 * Starts/resumes the timer. If timer is already running, no-op.
	 * 
	 */
	public void StartTimer() {
		if (!isRunning) {
			measureStartTime = Time.time;
			isRunning = true;
		}
	}

	/*
	 * PauseTimer()
	 * 
	 * Pauses the timer. If timer is not running, no-op.
	 * 
	 */
	public void PauseTimer() {
		if (isRunning) {
			isRunning = false;
			savedTime += Time.time - measureStartTime;
		}
	}

	/*
	 * ResetTimer()
	 * 
	 * Resets the timer to zero. Does not change running state of timer.
	 * 
	 */
	public void ResetTimer() {
		measureStartTime = Time.time;
		savedTime = 0;
	}
}
