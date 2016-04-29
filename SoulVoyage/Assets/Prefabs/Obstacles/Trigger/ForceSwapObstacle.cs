using UnityEngine;
using System.Collections;

public class ForceSwapObstacle : MonoBehaviour {

	public float effectDuration;
	public float effectDurationVar;
	private float trueEffectDuration;

	private float triggerTime;

	private ObstacleLifecycle lifecycleManager;
	private Pulsing pulse;
	private SpriteRenderer spriteRenderer;

	private State state = State.WAITING;
	private enum State {
		WAITING,
		ACTIVATED
	}

	void Start() {
		lifecycleManager = GetComponent<ObstacleLifecycle>();
		pulse = GetComponent<Pulsing>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		trueEffectDuration = effectDuration + Random.value * effectDurationVar;
	}

	void Update() {
		if (state == State.ACTIVATED) {
			float timeElapsed = Time.time - triggerTime;
			Color color = spriteRenderer.color;
			color.a = 1 - (timeElapsed / trueEffectDuration);
			spriteRenderer.color = color;

			if (timeElapsed >= trueEffectDuration) {
				GameManager.instance.inputManager.ReverseControllers();
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(Tag.Player) && state == State.WAITING) {
			lifecycleManager.enabled = false;
			pulse.enabled = false;
			triggerTime = Time.time;
			state = State.ACTIVATED;

			GameManager.instance.inputManager.ReverseControllers();
		}
	}

}
