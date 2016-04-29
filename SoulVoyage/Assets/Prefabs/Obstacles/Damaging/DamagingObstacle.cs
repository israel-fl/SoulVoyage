using UnityEngine;
using System.Collections;

public class DamagingObstacle : MonoBehaviour {

    [Header("Chasing Character")]
    public float maxChaseSpeed;
    public float minChaseSpeed;

    private GameObject character;

    private float chaseSpeed;

	void Start() {
        character = GameManager.instance.character;
		chaseSpeed = maxChaseSpeed;
	}

	void Update() {
        Vector3 diffVector = character.transform.position - transform.position;
        diffVector.Normalize();
		transform.position += (diffVector * chaseSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (GameManager.instance.playMode != GameManager.PlayMode.TUTORIAL
            && other.CompareTag(Tag.Player)) {
			GameManager.instance.DestroyCharacter();
		}
        else if (other.CompareTag("BlockingObstacle")) {
            chaseSpeed = minChaseSpeed;
        }
	}

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("BlockingObstacle")) {
            chaseSpeed = maxChaseSpeed;
        }
    }
}
