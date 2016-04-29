using UnityEngine;
using System.Collections;

public class DebugCharacterControls : MonoBehaviour {

	public float force;

	void Awake() {
		#if !UNITY_EDITOR
		Destroy(this);
		#endif
	}

	void Update() {
		if (Input.GetKey(KeyCode.RightArrow)) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(force, 0f, 0f));
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-force, 0f, 0f));
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, force, 0f));
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, -force, 0f));
		}
	}
}
