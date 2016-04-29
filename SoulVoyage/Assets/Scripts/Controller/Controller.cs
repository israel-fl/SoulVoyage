using UnityEngine;
using System.Collections;

/*
 * Obstacle
 * 
 * Script for managing Controllers and their effect on the character
 * 
 */

public class Controller : MonoBehaviour {

    public ParticleSystem pullForce;
	private ParticleSystem.EmissionModule emission;

	public enum ForceMode {
		ATTRACT,
		REPEL,
		OFF
	}

	[Tooltip("Magnitude of force to be applied while controller is held down.")]
    public float forceMagnitude = 1f;

	[Tooltip("Force mode (direction of force, or disabled)")]
	public ForceMode mode = ForceMode.ATTRACT;

	// Reference to Controller position
    private Vector3 controllerPosition;

	// Reference to character's Rigidbody2D component
    private Rigidbody2D characterRB;

	void Start () {
        controllerPosition = this.transform.position;
        controllerPosition.z = 0;
        characterRB = GameManager.instance.character.GetComponent<Rigidbody2D>();
		emission = pullForce.emission;
    }

    // Add Controller force to character
    public void ApplyForceToCharacter() {
		Vector3 forceVector;
		if (mode == ForceMode.ATTRACT) {
			// Force applied along vector from character to controller
			forceVector = controllerPosition - GameManager.instance.character.transform.position;
        }
        else if (mode == ForceMode.REPEL) {
			// Force applied along vector from controller to character
			forceVector = GameManager.instance.character.transform.position - controllerPosition;
		} else {
			// If disabled, no force applied
			return;
		}

        // Convert vector to 2D and normalize it
        Vector2 forceDirection = (new Vector2(forceVector.x, forceVector.y)).normalized;
        // Multiply vector by forceMagnitude and add it to character's RigidBody as force
        characterRB.AddForce(forceMagnitude * forceDirection);
    }

    // Called every frame
    private void Update() {

        Vector3 characterPosition = GameManager.instance.character.transform.position;
		float angle = Vector3.Angle(controllerPosition - characterPosition, Vector3.up);

        if (Vector3.Angle(controllerPosition - characterPosition, Vector3.right) < 90) {
            angle = -angle;
        }

        pullForce.transform.rotation = Quaternion.Euler(0, 0, angle);
        pullForce.startRotation = angle * 0.0174533f;
    }

    public void StartParticleSystem() {
        emission.enabled = true; // enable emmitter
    }

    public void StopParticleSystem() {
        emission.enabled = false; // disable emmitter
    }

	public void ReverseForce() {
		if (mode == ForceMode.ATTRACT) {
			mode = ForceMode.REPEL;
		} else if (mode == ForceMode.REPEL) {
			mode = ForceMode.ATTRACT;
		}
	}
}
