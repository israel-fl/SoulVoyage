using UnityEngine;
using System;
using System.Collections;

/*
 * ObstacleGenerator
 * 
 * Generates new obstacles at random and spawns them in the game.
 * 
 * Properties that should be taken into account when generating a new obstacle:
 * - Time since the last obstacle of that type was spawned.
 * - Proximity to main character (should not create obstacles on top of, or immediately next to, main character).
 * - Proximity to controllers (should not spawn under controllers).
 * - Number of obstacles in proximity of that obstacle. [NOT YET IMPLEMENTED]
 * - Number of other active obstacles of the same type. [NOT YET IMPLEMENTED]
 * - Has the minimum time to availability been met.
 * - Generation weight.
 * 
 */

public class ObstacleGenerator : MonoBehaviour {

	public Obstacle[] obstaclePrefabs;
	public GameObject[] enemySpawnPoints;

	private Obstacle[] activeObstacles;

	[Header("Generation Parameters")]
	[Space]
	[Tooltip("Minimum distance (in units) an obstacle will spawn relative to the character.")]
	public float minDistanceFromCharacter;
	[Tooltip("Minimum distance from controllers (measured from center).")]
	public float minDistanceFromControllers;
	[Space]
	[Tooltip("Minimum time between obstacle spawn events.")]
	public float spawnInterval;
	[Tooltip("Variability of spawn time.")]
	public float spawnIntervalVar; // TODO: Not currently used

	[Header("Starting Parameters")]
	public int initialCount;
	public int initialCountVar;

	[Header("Object References")]
	public GameObject[] controllers;

	/* Time-based spawning */
	private float lastSpawnTime = 0;

	/* Screen boundaries */
	private float screenBottomBoundary;
	private float screenHeight;
	private float screenLeftBoundary;
	private float screenWidth;

	/* Weighted obstacle selection */
	private int totalWeight = 0;
	private int availableObstacles = 0;

	/*
	 * Start()
	 * 
	 * Calculates screen boundaries. This is only calculated at start, so does not account for orientation changes yet.
	 * 
	 */
	void Start() {
		Camera camera = GameManager.instance.mainCamera;
		Debug.Assert(camera.orthographic);
		screenBottomBoundary = -1 * camera.orthographicSize;
		screenHeight = 2 * camera.orthographicSize;
		screenLeftBoundary = -1 * camera.orthographicSize * camera.aspect;
		screenWidth = 2 * camera.orthographicSize * camera.aspect;

		PrepareObstaclePrefabs();

		SpawnStartingObstacles();
	}

	/*
	 * 
	 * PrepareObstaclePrefabs()
	 * 
	 * Sort obstacle prefabs by time of availability and calculate starting weight.
	 * 
	 */
	private void PrepareObstaclePrefabs() {
		/* Sort obstacle prefabs by time of availability */
		Array.Sort(obstaclePrefabs, delegate(Obstacle x, Obstacle y) {
			return x.minTimeToAvailability.CompareTo(y.minTimeToAvailability);
		});

		/* Calculate number of available obstacles at start. */
		while (availableObstacles < obstaclePrefabs.Length && obstaclePrefabs[availableObstacles].minTimeToAvailability == 0) {
			totalWeight += obstaclePrefabs[availableObstacles].generationWeight;
			availableObstacles++;
		}
		Debug.Log("Available obstacles: " + availableObstacles);
	}

	/*
	 * SpawnStartingObstacles()
	 * 
	 * Spawns an initial set of obstacles at the start of the round.
	 * 
	 * Currently spawns via the same parameters as Update(), but in future this will be updated
	 * to create a more fair initial play field.
	 * 
	 */
	private void SpawnStartingObstacles() {
		int numInitialObstacles = initialCount + UnityEngine.Random.Range(0, initialCountVar);
		for (int i = 0 ; i < numInitialObstacles ; i++) {
			Obstacle nextObstacle = NextObstacleToSpawn();

			if (nextObstacle.IsStaticObstacle()) {
				Spawn(nextObstacle.gameObject, NextStaticSpawnLocation());
			} else {
				// TODO: Implement
			}
		}
	}

	/*
	 * Update()
	 * 
	 * Determines whether to spawn a new obstacle, and if so calls appropriate methods.
	 * 
	 * Currently based only on time
	 * 
	 */
	void Update() {
		// Since only static obstacles have been implemented at this time, does not account for obstacle types
		if (Time.time - lastSpawnTime > spawnInterval) {
			Obstacle nextObstacle = NextObstacleToSpawn();

			if (nextObstacle.IsStaticObstacle()) {
				Spawn(nextObstacle.gameObject, NextStaticSpawnLocation());
			} else {
				// TODO: Implement
			}
		}
	}

	/*
	 * NextObstacleToSpawn()
	 * 
	 * Returns the next obstacle to spawn.
	 * 
	 * Currently selects from all obstacle prefabs at random.
	 * 
	 */
	private Obstacle NextObstacleToSpawn() {
		/* Expand available obstacles if able */
		while (availableObstacles < obstaclePrefabs.Length && obstaclePrefabs[availableObstacles].minTimeToAvailability <= Time.time) {
			totalWeight += obstaclePrefabs[availableObstacles].generationWeight;
			availableObstacles++;
		}

		Obstacle nextObstacle;
		do {
			int targetWeight = UnityEngine.Random.Range(0, totalWeight);
			Debug.Log("Target weight: " + targetWeight);
			int index = 0, weightSum = 0;
			for (int i = 0 ; i < availableObstacles ; i++) {
				if (weightSum < targetWeight) {
					index = i;
				}
				weightSum += obstaclePrefabs[i].generationWeight;
			}
			Debug.Log("Next obstacle of type " + index);
			nextObstacle = obstaclePrefabs[index];
		} while (!(
			/* Check that obstacle has met minimum time requirement. */
			nextObstacle.minTimeToAvailability <= Time.time
		));
		return nextObstacle;
	}

	/*
	 * NextActiveSpawnPoint()
	 * 
	 * Returns the next spawn point to use for enemies.
	 * 
	 * Currently selects from all points at random.
	 * 
	 */
	private GameObject NextActiveSpawnPoint() {
		return enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Length)];
	}

	/*
	 * NextStaticSpawnLocation()
	 * 
	 * Returns the next spawn location to use on the screen.
	 * 
	 * Currently selects a point on the screen at random.
	 * 
	 */
	private Vector3 NextStaticSpawnLocation() {
		Vector3 spawnLocation;
		do {
			spawnLocation = new Vector3(screenLeftBoundary + UnityEngine.Random.value * screenWidth, screenBottomBoundary + UnityEngine.Random.value * screenHeight, 0);
		} while (!(
			/* Check proximity to character. */
			(spawnLocation - GameManager.instance.character.transform.position).magnitude >= minDistanceFromCharacter &&

			/* Check proximity to controller 1. */
			(spawnLocation - controllers[0].transform.position).magnitude >= minDistanceFromControllers &&

			/* Check proximity to controller 2. */
			(spawnLocation - controllers[1].transform.position).magnitude >= minDistanceFromControllers &&

			/* Check proximity to controller 3. */
			(spawnLocation - controllers[2].transform.position).magnitude >= minDistanceFromControllers &&

			/* Check proximity to controller 4. */
			(spawnLocation - controllers[3].transform.position).magnitude >= minDistanceFromControllers
		));
		return spawnLocation;
	}

	/*
	 * Spawn(prefab, location)
	 * 
	 * Spawn a new instance of prefab at given position.
	 * 
	 */
	private void Spawn(GameObject prefab, Vector3 position) {
		GameObject instance = Instantiate(prefab);
		instance.transform.position = position;

		lastSpawnTime = Time.time;
	}
}
