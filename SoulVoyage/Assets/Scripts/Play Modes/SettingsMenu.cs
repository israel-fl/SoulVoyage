using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    public Toggle musicToggle;
    public Toggle vibrateToggle;
    public Slider musicVolume;

    public AudioSource settingsMusic;

    private float currentVolume;

    void Start() {

        currentVolume = PlayerPrefs.GetFloat(PreferenceKeys.GameMusicVolumeKey);

        musicVolume.value = currentVolume; // set slider to saved value

        // set settings game volume
        settingsMusic.volume = currentVolume;

        // This converts 0 and null to False and everything ELSE to true
        bool currentVolumeState = Convert.ToBoolean(PlayerPrefs.GetInt(PreferenceKeys.VolumeStateKey, 1));

        if (currentVolumeState) {
            musicToggle.isOn = true; // set visual state of toggle
            PlayMusic();
        } 
        else {
            musicToggle.isOn = false; // set visual state of toggle
            PauseMusic();
        }

        bool currentVibrationState = Convert.ToBoolean(PlayerPrefs.GetInt(PreferenceKeys.VibrateStateKey, 1));

        if (currentVibrationState) {
            vibrateToggle.isOn = true;
        } 
        else {
            vibrateToggle.isOn = false;
        }
        

    }


    public void SetMusic() {

        //GameManager.instance.SetMusic(enable);
        //Debug.Log("SetMusic called");

        // enable
        if (musicToggle.isOn) {

            PlayerPrefs.SetInt(PreferenceKeys.VolumeStateKey, EnableState.ENABLED);
            PlayMusic();
            
        }
        // disable
        else {

            PlayerPrefs.SetInt(PreferenceKeys.VolumeStateKey, EnableState.DISABLED);
            PauseMusic();

        }

    }

    public void SetVibrations() {

        // enable
        if (vibrateToggle.isOn) {
            PlayerPrefs.SetInt(PreferenceKeys.VibrateStateKey, EnableState.ENABLED);
        }
        // Disable
        else {
            PlayerPrefs.SetInt(PreferenceKeys.VibrateStateKey, EnableState.DISABLED);
        }

    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(Scenes.MainMenuScene);
    }

    private void PlayMusic() {
        settingsMusic.mute = false;
    }

    private void PauseMusic() {
        settingsMusic.mute = true;
    }

    public void ChangeVolume(float volumeLevel) {

        settingsMusic.volume = volumeLevel;
        PlayerPrefs.SetFloat(PreferenceKeys.GameMusicVolumeKey, volumeLevel);

    }
    
}
