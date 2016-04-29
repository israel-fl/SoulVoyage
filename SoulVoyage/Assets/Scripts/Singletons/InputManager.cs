using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Controller[] controllers;

	void Update() {
        // Make sure emmitter is stopped
        controllers[0].StopParticleSystem();
        controllers[1].StopParticleSystem();
        controllers[2].StopParticleSystem();
        controllers[3].StopParticleSystem();

        int numTouches = Input.touchCount;
		if (numTouches > 0) {
			for (int i = 0; i < numTouches; i++)
			{
				Touch touch = Input.GetTouch(i);
				CollideWithTouch(touch.position, touch.phase);
              
			}
		} else if (Input.GetMouseButton(0)) {

			CollideWithTouch(Input.mousePosition, TouchPhase.Began);

        } else if (!Input.GetMouseButton(0)) {
            CollideWithTouch(Input.mousePosition, TouchPhase.Ended);
        }
	}

	public void ReverseControllers() {
		controllers[0].ReverseForce();
		controllers[1].ReverseForce();
		controllers[2].ReverseForce();
		controllers[3].ReverseForce();
	}

	void CollideWithTouch(Vector3 touchPosition, TouchPhase phase) {
		Vector3 pos = Camera.main.ScreenToWorldPoint(touchPosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit.collider != null) {
			GameObject objectHit = hit.collider.gameObject;
			if (objectHit.tag == "Controller") {
                Controller controller = objectHit.GetComponent<Controller>();

                if (phase == TouchPhase.Began ||
                    phase == TouchPhase.Stationary ||
                    phase == TouchPhase.Moved) {

                    controller.ApplyForceToCharacter();

                    // Start Particle System on collision
                    controller.StartParticleSystem();
                }

                else if (phase == TouchPhase.Ended) {
                    // Stop Particle system on touch release
                    controller.StopParticleSystem();
                }

            }
		}
	}

}
