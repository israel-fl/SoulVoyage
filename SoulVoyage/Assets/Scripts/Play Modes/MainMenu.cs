using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MainMenu : MonoBehaviour {

    public AudioSource menuMusic;

    private float currentVolume;

    void Start() {

        currentVolume = PlayerPrefs.GetFloat(PreferenceKeys.GameMusicVolumeKey, 1);

        // set menu volume
        menuMusic.volume = currentVolume;

        bool currentVolumeState = Convert.ToBoolean(PlayerPrefs.GetInt(PreferenceKeys.VolumeStateKey, 1));

        if (currentVolumeState) {
            menuMusic.mute = false;
        }
        else {
            menuMusic.mute = true;
        }

    }

	public void GoToFreePlay() {
		SceneManager.LoadScene(Scenes.FreePlayScene);
	}

    public void GoToSeeker() {
        SceneManager.LoadScene(Scenes.SeekerScene);
    }

    public void GoToTutorial() {
        SceneManager.LoadScene(Scenes.ControlsTutorialScene);
    }

    public void GoToSettings() {
        SceneManager.LoadScene(Scenes.SettingsScene);
    }
}
