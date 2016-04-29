using UnityEngine;
using System.Collections;

public delegate void DestroyCharacterEffectComplete();

public class DestroyCharacterEffect : MonoBehaviour {

	public float expansionRate;
	public float finalExpansion;

	public DestroyCharacterEffectComplete completionHandler;

	void Update() {
		if (transform.localScale.x < finalExpansion) {
			Vector3 scale = transform.localScale;
			scale.x += expansionRate;
			scale.y += expansionRate;
			transform.localScale = scale;
		} else {
			completionHandler();
		}
	}
}
