using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	public GameObject testeroid;
	public int numtospawn = 5;
	public int minRange = 1300;
	public int maxRange = 2400;
	public int maxMagnitude = 200;
	public int speedLimit = 10;

	// Use this for initialization
	void Start () {
		spawnAsteroids(numtospawn);
	}

	void spawnAsteroids(int numberToSpawn) {
		for (int i = 0; i < numberToSpawn; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(testeroid);
			float ra = (float) Random.Range(minRange, maxRange);
			float rb = (float) Random.Range(minRange, maxRange);
			float spd = (float) Random.Range(-speedLimit, speedLimit);
			float rt = (float) Random.Range(0,360);
			float phase = (float) Random.Range(0, 2);
			float magnitude = (float) Random.Range(0, maxMagnitude);
			float angle = (float) Random.Range(0, 360);
			Vector3 v = new Vector3(0,0,0);

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra, rb, spd, rt, phase, magnitude, angle, v);
		}
	}
}
