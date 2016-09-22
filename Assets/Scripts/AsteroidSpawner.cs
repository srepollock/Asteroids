using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	public GameObject testeroid;
	public int numtospawn = 5;

	// Use this for initialization
	void Start () {
		spawnAsteroids(numtospawn);
	}

	void spawnAsteroids(int numberToSpawn) {
		for (int i = 0; i < numberToSpawn; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(testeroid);
			float ra = (float) Random.Range(20, 60);
			float rb = (float) Random.Range(20, 60);
			float spd = (float) Random.Range(-150, 150);
			float rt = (float) Random.Range(0,360);
			float phase = (float) Random.Range(0, 2);
			float magnitude = (float) Random.Range(0, 20);
			float angle = (float) Random.Range(0, 360);
			Vector3 v = new Vector3(0,0,0);

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra, rb, spd, rt, phase, magnitude, angle, v);
		}
	}
}
