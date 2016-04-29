using UnityEngine;
using System.Collections;

public interface ObstacleLifecycleListener {
	// Called when the lifecycle state changes from Appearing to Active
	void OnLifecycleActive();

	// Called when the lifecycle state changes from Active to Disappearing
	void OnLifecycleDisappear();

	// Called each update cycle while the lifecycle state is Active
	void ActiveUpdate();
}

public class ObstacleLifecycle : MonoBehaviour {

	public float staticScale;

	[Header("Lifetime")]
	public float minLifetime;
	public float lifetimeVariability;

	private float creationTime;

	[Header("Appearing Animation")]
	public float appearRate;

	[Header("Disappearing Animation")]
	public float disappearRate;

	private enum LifeCyclePhase {
		APPEARING,
		ACTIVE,
		DISAPPEARING
	}
	private LifeCyclePhase phase;

	// Pulsing component [OPTIONAL]
	private Pulsing pulse;

	public ObstacleLifecycleListener listener;

	void Start() {
		transform.localScale = new Vector3(0, 0, 1);
		phase = LifeCyclePhase.APPEARING;
		creationTime = Time.time;

		// Set up pulsing
		pulse = GetComponent<Pulsing>();
		if (pulse != null) {
			pulse.enabled = false;
		}
	}

	void Update () {
		if (phase == LifeCyclePhase.ACTIVE) {
			float timeAlive = Time.time - creationTime;
			if (GameManager.instance.playMode != GameManager.PlayMode.TUTORIAL && 
                timeAlive > minLifetime && Random.value < (timeAlive - minLifetime) / lifetimeVariability) {
				// Begin the disappearing phase
				phase = LifeCyclePhase.DISAPPEARING;

				if (pulse != null) {
					pulse.enabled = false;
				}

				if (listener != null) {
					listener.OnLifecycleDisappear();
				}
			} else if (listener != null) {
				listener.ActiveUpdate();
			}
		} else if (phase == LifeCyclePhase.APPEARING) {
			// Appear
			if (transform.localScale.x < staticScale) {
				Vector3 scale = transform.localScale;
				scale.x += appearRate;
				scale.y += appearRate;
				transform.localScale = scale;
			} else {
				// Appearing phase has completed
				phase = LifeCyclePhase.ACTIVE;

				if (pulse != null) {
					pulse.enabled = true;
				}

				if (listener != null) {
					listener.OnLifecycleActive();
				}
			}
		} else {
			// Disappear
			if (transform.localScale.x > 0) {
				Vector3 scale = transform.localScale;
				scale.x -= disappearRate;
				scale.y -= disappearRate;
				transform.localScale = scale;
			} else {
				Destroy(gameObject);
			}
		}
	}
}
