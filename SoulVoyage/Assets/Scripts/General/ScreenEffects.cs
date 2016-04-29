using UnityEngine;
using System.Collections;

public class ScreenEffects : MonoBehaviour {

    public ParticleSystem leftWall;
    public ParticleSystem rightWall;
    public ParticleSystem topWall;
    public ParticleSystem bottomWall;

    private ParticleSystem.EmissionModule leftEmission;
    private ParticleSystem.EmissionModule rightEmission;
    private ParticleSystem.EmissionModule topEmission;
    private ParticleSystem.EmissionModule bottomEmission;

    public bool enableParticles = true;
    public bool enableVibrate;
    private const int ENABLE_VIBRATE = 1; // true
    private const int DISABLE_VIBRATE = 0; // false

    // Use this data for initialization
    void Start() {

        int currentVibrationState = PlayerPrefs.GetInt(PreferenceKeys.VibrateStateKey, 1);
        if (currentVibrationState == ENABLE_VIBRATE) {
            enableVibrate = true;
        } 
        else if (currentVibrationState == DISABLE_VIBRATE) {
            enableVibrate = false;
        }

        leftEmission = leftWall.emission;
        rightEmission = rightWall.emission;
        topEmission = topWall.emission;
        bottomEmission = bottomWall.emission;

        leftEmission.enabled = false;
        rightEmission.enabled = false;
        topEmission.enabled = false;
        bottomEmission.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll) {

        if (enableParticles) {

            // Enable Particles
			if (coll.gameObject.CompareTag(Tag.LeftWall)) {
                leftEmission.enabled = true;
            }
			else if (coll.gameObject.CompareTag(Tag.RightWall)) {
                rightEmission.enabled = true;
            }
			else if (coll.gameObject.CompareTag(Tag.TopWall)) {
                topEmission.enabled = true;
            }
			else if (coll.gameObject.CompareTag(Tag.BottomWall)) {
                bottomEmission.enabled = true;
            }

        }

        if (enableVibrate) {
            // Enable Vibration on devices
			if (coll.gameObject.CompareTag(Tag.LeftWall) ||
				coll.gameObject.CompareTag(Tag.RightWall) ||
				coll.gameObject.CompareTag(Tag.TopWall) ||
				coll.gameObject.CompareTag(Tag.BottomWall)) {

                 Handheld.Vibrate();
                

            }
        }

    }

    void OnCollisionExit2D(Collision2D coll) {

        // Disable Particles
		if (coll.gameObject.CompareTag(Tag.LeftWall)) {
            leftEmission.enabled = false;
        }
		else if (coll.gameObject.CompareTag(Tag.RightWall)) {
            rightEmission.enabled = false;
        }
		else if (coll.gameObject.CompareTag(Tag.TopWall)) {
            topEmission.enabled = false;
        }
		else if (coll.gameObject.CompareTag(Tag.BottomWall)) {
            bottomEmission.enabled = false;
        }


    }

}

