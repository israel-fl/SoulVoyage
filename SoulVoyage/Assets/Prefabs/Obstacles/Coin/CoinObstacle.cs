using UnityEngine;
using System.Collections;


public class CoinObstacle : MonoBehaviour {

    public Score score;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tag.Player)) {
            GameManager.instance.score.IncrementScore();
            Destroy(gameObject);
        }
    }

}
