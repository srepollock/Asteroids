using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	public GameObject testeroid;
	public GameObject[] smallroids;
	public GameObject[] medroids;
	public GameObject[] largeroids;
	public int numtospawn = 5;
	public int minRange = 1300;
	public int maxRange = 2400;
	public int maxMagnitude = 200;
	public int speedLimit = 10;
    public int asteroidLevelScaling = 10;
	public int curAsteroids = 0;

	// Use this for initialization
	void Start () {
		// spawnAsteroids(numtospawn);
		// get player prefs
		int curlvl = PlayerPrefs.GetInt("currentlevel");
		
		spawnSmallAsteroids(curlvl * asteroidLevelScaling);
		spawnMediumAsteroids(curlvl * asteroidLevelScaling / 2);
		spawnLargeAsteroids(curlvl * asteroidLevelScaling / 10);

		curAsteroids = (curlvl * asteroidLevelScaling) + (curlvl * asteroidLevelScaling / 2) + (curlvl * asteroidLevelScaling / 10);

		// testing
		spawnMediumAsteroids(10);
		curAsteroids += 10;

        Debug.Log("CurrentLevel = " + curlvl);
	}

	void spawnSmallAsteroids(int numberToSpawn) {
		for (int i = 0; i < numberToSpawn; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(smallroids[i % smallroids.Length]);
			float ra = (float) Random.Range(minRange, maxRange);
			float rb = (float) Random.Range(minRange, maxRange);
			float spd = (float) Random.Range(-speedLimit, speedLimit);
			float rt = (float) Random.Range(0,360);
			float phase = (float) Random.Range(0, 2);
			float magnitude = (float) Random.Range(0, maxMagnitude);
			float angle = (float) Random.Range(0, 360);
			Vector3 v = new Vector3(0,0,0);

			// stops asteroids from not moving at all (or moving at a very low speed)
			while ((spd <= 1) && (spd >= -1)) {
				spd = (float) Random.Range(-speedLimit, speedLimit);
			}

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra, rb, spd, rt, phase, magnitude, angle, v);
		}
	}

	void spawnMediumAsteroids(int numberToSpawn) {
		for (int i = 0; i < numberToSpawn; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(medroids[i % medroids.Length]);
			float ra = (float) Random.Range(minRange, maxRange);
			float rb = (float) Random.Range(minRange, maxRange);
			float spd = (float) Random.Range(-speedLimit/2, speedLimit/2);
			float rt = (float) Random.Range(0,360);
			float phase = (float) Random.Range(0, 2);
			float magnitude = (float) Random.Range(0, maxMagnitude);
			float angle = (float) Random.Range(0, 360);
			Vector3 v = new Vector3(0,0,0);

			// stops asteroids from not moving at all (or moving at a very low speed)
			while ((spd <= 1) && (spd >= -1)) {
				spd = (float) Random.Range(-speedLimit, speedLimit);
			}

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra, rb, spd, rt, phase, magnitude, angle, v);
		}
	}

	void spawnLargeAsteroids(int numberToSpawn) {
		for (int i = 0; i < numberToSpawn; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(testeroid);
			float ra = (float) Random.Range(minRange, maxRange);
			float rb = (float) Random.Range(minRange, maxRange);
			float spd = (float) Random.Range(-speedLimit/3, speedLimit/3);
			float rt = (float) Random.Range(0,360);
			float phase = (float) Random.Range(0, 2);
			float magnitude = (float) Random.Range(0, maxMagnitude);
			float angle = (float) Random.Range(0, 360);
			Vector3 v = new Vector3(0,0,0);

			// stops asteroids from not moving at all (or moving at a very low speed)
			while ((spd <= 1) && (spd >= -1)) {
				spd = (float) Random.Range(-speedLimit, speedLimit);
			}

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra, rb, spd, rt, phase, magnitude, angle, v);
		}
	}

	// this is here on purpose - if we decide we want to NOT explode asteroids when the player collides with one
	public void asteroidDestroyed() {
		curAsteroids--;
	}

	// we can use this later on where the player hits asteroids if we want them to explode
    public void asteroidDestroyed(string size) {
        if (size == "Small") {
        	curAsteroids--;
        }
        if (size == "Medium" || size == "Large") {
        	curAsteroids = curAsteroids + 3;
        }
    }

    // always explode into 4 of smaller size
    public void explodeAsteroid(string size, float ra, float rb, float spd, float rt, float phase, float magnitude, float agl, Vector3 v) {
    	string newSize = "Small";
    	if (size == "Large") {
    		newSize = "Medium";
    	}

    	// we can change the spd, but for ra and rb can only change slightly. DO NOT CHANGE PHASE
    	// magnitude can change a bit.
    	for (int i = 0; i < 4; i++) {
			// spawn a random asteroid
			GameObject asteroidclone = Instantiate(testeroid);
			// random radius change
			float n_ra = (float) Random.Range(-30, 30);
			float n_rb = (float) Random.Range(-30, 30);
			float n_spd = (float) Random.Range(1, 6);
			float n_rt = (float) Random.Range(-5, 5);
			float n_magnitude = (float) Random.Range(-40, 40);

			int n_agl;
			// spawn 1 in original angle, and then 1 with +5, 1 with -5, and 1 with + or - 10
			switch (i) {
				case 0:
					// no change in angle
					n_agl = 0;
				break;
				case 1:
					n_agl = 5;
				break;
				case 2:
					n_agl = -5;
				break;
				case 3:
					n_agl = 10;
					if (Random.value > 0.5f) {
						n_agl *= -1;
					}
				break;
				default:
					n_agl = 0;
				break;
			}

			// decide if we need to change the direction of the roid
			if (Random.value > 0.5f) {
				spd *= -1;
			}

			// speed always increases when exploding
			if (spd < 0) {
				n_spd *= -1;
			}

			asteroidclone.GetComponent<Eliptical_movement>().setValues(ra + n_ra, rb + n_rb, spd + n_spd, rt, phase, magnitude + n_magnitude, agl, v);
			asteroidclone.GetComponent<DestroyByContact>().setSize(newSize);
		}
    }
}
