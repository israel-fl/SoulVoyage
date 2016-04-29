using UnityEngine;
using System.Collections;
using System;

/*
 * GameManager
 * 
 * Singleton game manager.  Stores references to other singletons.
 * 
 * Keeps track of what play mode is currently being used, and spreads commands to the appropriate play mode manager.
 * 
 */

public class GameManager : MonoBehaviour {

	public enum PlayMode {
		FREE_PLAY,
        SEEKER_PLAY,
        TUTORIAL
	}

	public static GameManager instance = null;

	public PlayMode playMode;

	public PlayManager playManager { get; private set; }
	public InputManager inputManager { get; private set; }
	public GameObject character;
	public ObstacleGenerator obstacleGenerator;
	public Camera mainCamera;
	public AudioSource backgroundMusicSource;

    public Score score;

    [Header("User Interface")]
	[Tooltip("UI elements to be hidden on end of game.")]
	public GameObject[] interfaceElements;

	[Header("Animation Effects")]
	public DestroyCharacterEffect destroyCharacterEffect;

	[HideInInspector]
	public bool gameOver = false;

	private FreePlayGameManager freePlayGameManager;

    private SeekerPlayGameManager seekerPlayGameManager;

    private TutorialManager tutorialManager;

    private float currentVolume;

    /*
	 * Awake()
	 * 
	 * Enforces singleton. All other logic should be in Setup().
	 * 
	 */
    void Awake() {
		// Enforce singleton
		if (instance == null) {
			instance = this;
			Setup();
		} else if (instance != this) {
			Destroy(gameObject);
		}
	}

	/*
	 * Setup()
	 * 
	 * Internal GameObject setup logic. Called by Awake() once.
	 * 
	 */
	void Setup() {
        inputManager = GetComponent<InputManager>();

        if (playMode != PlayMode.TUTORIAL) {
            playManager = GetComponent<PlayManager>();

            if (playMode == PlayMode.FREE_PLAY) {
                freePlayGameManager = GetComponent<FreePlayGameManager>();
            } else if (playMode == PlayMode.SEEKER_PLAY) {
                seekerPlayGameManager = GetComponent<SeekerPlayGameManager>();
            }
        } else {
            tutorialManager = GetComponent<TutorialManager>();
        }
	}

	void Start() {

        currentVolume = PlayerPrefs.GetFloat(PreferenceKeys.GameMusicVolumeKey);

        // set menu volume
        backgroundMusicSource.volume = currentVolume;

        bool currentVolumeState = Convert.ToBoolean(PlayerPrefs.GetInt(PreferenceKeys.VolumeStateKey, 1));

        if (currentVolumeState) {
            backgroundMusicSource.mute = false;
        }
        else {
            backgroundMusicSource.mute = true;
        }

        if (PlayerPrefs.HasKey(PreferenceKeys.GameMusicVolumeKey)) {
			backgroundMusicSource.volume = PlayerPrefs.GetFloat(PreferenceKeys.GameMusicVolumeKey);
		}
	}

	/*
	 * DestroyCharacter()
	 * 
	 * Initiates the destroy character effect and ends the game on completion.
	 *
	 */
	public void DestroyCharacter() {
		GameObject dcego = Instantiate(destroyCharacterEffect.gameObject);
		Vector3 position = character.transform.position;
		position.z = -2;
		dcego.transform.position = position;

		DestroyCharacterEffect dce = dcego.GetComponent<DestroyCharacterEffect>();
		if (playMode == PlayMode.FREE_PLAY) {
			freePlayGameManager.timer.PauseTimer();
			dce.completionHandler = freePlayGameManager.GameOver;
		} 
        else if (playMode == PlayMode.SEEKER_PLAY) {
            dce.completionHandler = seekerPlayGameManager.GameOver;
        }

		for (int i = 0 ; i < interfaceElements.Length ; i++) {
			interfaceElements[i].SetActive(false);
		}
	}

	public void SetBackgroundMusicVolume(float volume) {
		backgroundMusicSource.volume = volume;
		PlayerPrefs.SetFloat(PreferenceKeys.GameMusicVolumeKey, volume);
	}

}
