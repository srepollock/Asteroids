using UnityEngine;
using System.Collections;

public class MediumAsteroid : MonoBehaviour {

	public static int ASTEROIDDAMAGE = SmallAsteroid.ASTEROIDDAMAGE * 4 + ASTEROIDHEALTH;
	public static int ASTEROIDHEALTH = 200;

	int currentHealth;
	GameObject sancho;
	AsteroidSpawner asteroidSpawner;
    Eliptical_movement moveScript;
	void Start() {
		currentHealth = ASTEROIDHEALTH;
		sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
        moveScript = GetComponent<Eliptical_movement>();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			
		}
		if (col.gameObject.tag == "Shot") {
			TakeDamage(PlayerShot.SHOTDAMAGE);
			if (currentHealth <= 0) {
				SpawnSmall();
			}
		}
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}

	void SpawnSmall() {
		Destroy(gameObject); //Destroy object this script is attatched to
		asteroidSpawner.asteroidDestroyed("Medium"); //Increase amount of asteroids
		asteroidSpawner.explodeAsteroid("Medium", moveScript.radiusA, moveScript.radiusB, 
										moveScript.speed, moveScript.rtilt, moveScript.atilt_phase, moveScript.atilt_severity, 
										moveScript.angle, moveScript.center);
		Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);
	}
}
