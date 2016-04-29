using UnityEngine;
using System.Collections;

/*
 * Obstacle
 * 
 * Parent class for obstacles.  Adds properties for use in obstacle generation.
 * 
 */

public class Obstacle : MonoBehaviour {

	public enum ObstacleType {
		BLOCKING,	// Blocking obstacles stop the player from moving past, but do no damage
		FORCE,		// Force obstacles apply a force (either attractive or repulsive) to the player
		TRIGGER,	// Trigger obstacles have an effect when the player hits
		DAMAGING,	// Damaging obstacles will do damage when the player hits, but are static
		ENEMY,		// Enemies move around and actively try to attack the player
        COIN        // Coin gives the player points on collision
	};

	[Tooltip("Type of obstacle (should match behavior category)")]
	public ObstacleType obstacleType;

	[Header("Generation")]
	[Tooltip("Minimum time (in seconds) into the game when this obstacle becomes available")]
	public float minTimeToAvailability = 0f;
	[Tooltip("Weight for random selection of obstacle")]
	public int generationWeight = 1;


	/* CONVENIENCE METHODS */

	public bool IsStaticObstacle() {
        return obstacleType == ObstacleType.BLOCKING ||
            obstacleType == ObstacleType.FORCE ||
            obstacleType == ObstacleType.TRIGGER ||
            obstacleType == ObstacleType.DAMAGING ||
            obstacleType == ObstacleType.COIN;
	}
}
