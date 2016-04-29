using UnityEngine;
using System.Collections;

public class Pulsing : MonoBehaviour {

	public float scaleDelta;
	public float duration;

	private Vector3 baseScale;
	private float animationTimeProgress;

	void Start() {
		baseScale = gameObject.gameObject.transform.localScale;
	}

	void Update() {
		animationTimeProgress += Time.deltaTime;
		while (animationTimeProgress > duration) {
			animationTimeProgress -= duration;
		}

		Vector3 scale = gameObject.transform.localScale;
		if (animationTimeProgress < duration / 2) {
			// Expanding
			float expandProgress = (animationTimeProgress / duration) * 2;

			scale.x = baseScale.x + (expandProgress * scaleDelta);
			scale.y = baseScale.y + (expandProgress * scaleDelta);
		} else {
			// Shrinking
			float shrinkProgress = (animationTimeProgress / duration - 0.5f) * 2;

			scale.x = baseScale.x + ((1 - shrinkProgress) * scaleDelta);
			scale.y = baseScale.y + ((1 - shrinkProgress) * scaleDelta);
		}
		gameObject.transform.localScale = scale;
	}

}
