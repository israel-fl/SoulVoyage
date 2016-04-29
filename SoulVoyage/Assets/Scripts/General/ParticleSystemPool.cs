using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemPool : MonoBehaviour {

	public int initialSize;
	public GameObject prefab;

	private Queue<GameObject> objectQueue;

	void Awake () {
		objectQueue = new Queue<GameObject>();
		for (int i = 0 ; i < initialSize ; i++) {
			objectQueue.Enqueue(NewObject());
		}
	}

	public GameObject GetObject(Color color) {
		GameObject go;
		if (objectQueue.Count > 0) {
			go = objectQueue.Dequeue();
		} else {
			go = NewObject();
		}

		go.GetComponent<ParticleSystem>().startColor = color;

		go.SetActive(true);
		return go;
	}

	public void ReturnObject(GameObject go) {
		go.SetActive(false);
		objectQueue.Enqueue(go);
	}

	private GameObject NewObject() {
		GameObject instance = Instantiate(prefab);
		instance.name = "Force Particles (Pooled)";
		instance.SetActive(false);
		instance.transform.parent = GameManager.instance.character.transform;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localScale = Vector3.one;
		return instance;
	}
}
