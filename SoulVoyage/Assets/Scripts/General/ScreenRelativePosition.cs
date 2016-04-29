using UnityEngine;
using System.Collections;

/*
 * ScreenRelativePosition
 * 
 * On start, sets the position of the object relative to the screen, then removes the component.
 * 
 * Camera must be using orthographic projection.
 * 
 */

public class ScreenRelativePosition : MonoBehaviour {

	public enum ScreenSide { LEFT, RIGHT, TOP, BOTTOM };

	public new Camera camera;
	public ScreenSide side;
	public float offset;

	void Start () {
		if (camera == null) {
			camera = Camera.main;
		}

		if (!camera.orthographic) {
			Debug.LogError("ScreenRelativePosition only works with orthographic cameras");
			Destroy(this);
			return;
		}

		Vector3 position = transform.position;

		if (side == ScreenSide.LEFT) {
			position.x = -(camera.orthographicSize * camera.aspect) + offset;
		} else if (side == ScreenSide.RIGHT) {
			position.x = (camera.orthographicSize * camera.aspect) + offset;
		} else if (side == ScreenSide.TOP) {
			position.y = camera.orthographicSize + offset;
		} else {
			position.y = -camera.orthographicSize + offset;
		}

		transform.position = position;
		Destroy(this);
	}

}
