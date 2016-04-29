using UnityEngine;
using System.Collections;

public class ForceObstacle : MonoBehaviour, ObstacleLifecycleListener {

	[Header("Force Parameters")]
	public float forceMagnitude;

	private Rigidbody2D characterRB;
	private ParticleSystem forceParticleSystem;
	private ParticleSystem.EmissionModule forceParticleSystemEmission;

	void Start() {
		// Start listening for lifecycle updates
		GetComponent<ObstacleLifecycle>().listener = this;

		characterRB = GameManager.instance.character.GetComponent<Rigidbody2D>();
		forceParticleSystem = GameManager.instance.character.GetComponent<ParticleSystemPool>()
            .GetObject(GetComponent<SpriteRenderer>().color).GetComponent<ParticleSystem>();
		forceParticleSystemEmission = forceParticleSystem.emission;
	}

	public void OnLifecycleActive() {
		forceParticleSystemEmission.enabled = true;
	}

	public void OnLifecycleDisappear() {
		forceParticleSystemEmission.enabled = false;
		GameManager.instance.character.GetComponent<ParticleSystemPool>().ReturnObject(forceParticleSystem.gameObject);
	}

	public void ActiveUpdate() {
		// Apply force
		Vector3 forceVector = gameObject.transform.position - GameManager.instance.character.transform.position;
		Vector2 forceDirection = (new Vector2(forceVector.x, forceVector.y)).normalized;
		characterRB.AddForce(forceMagnitude * forceDirection);

		// Update particle system
		Vector3 characterPosition = GameManager.instance.character.transform.position;
		float angle = Vector3.Angle(transform.position - characterPosition, Vector3.up);
		if (Vector3.Angle(transform.position - characterPosition, Vector3.right) < 90) {
			angle = -angle;
		}
		forceParticleSystem.transform.rotation = Quaternion.Euler(0, 0, angle);
		forceParticleSystem.startRotation = angle * 0.0174533f;
	}

}
