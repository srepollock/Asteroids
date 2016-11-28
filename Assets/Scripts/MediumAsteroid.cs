using UnityEngine;
using System.Collections;

public class MediumAsteroid : MonoBehaviour {

	public static int ASTEROIDDAMAGE = SmallAsteroid.ASTEROIDDAMAGE * 4 + ASTEROIDHEALTH;
	public static int ASTEROIDHEALTH = 200;
	public static int ASTEROIDSCORE = 20;

	int currentHealth;
	GameObject sancho;
	AsteroidSpawner asteroidSpawner;
    Eliptical_movement moveScript;
	bool isDead = false;
	void Start() {
		currentHealth = ASTEROIDHEALTH;
		sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
        moveScript = GetComponent<Eliptical_movement>();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(ASTEROIDDAMAGE - currentHealth);
			Death();
		}
		if (col.gameObject.tag == "Shot") {
			TakeDamage(PlayerShot.SHOTDAMAGE);
			if (currentHealth <= 0 && !isDead) {
				isDead = true;
				SpawnSmall();
			}
		}
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}

	void SpawnSmall() {
		asteroidSpawner.asteroidDestroyed(); 
		asteroidSpawner.explodeAsteroid("Medium", moveScript.radiusA, moveScript.radiusB, 
										moveScript.speed, moveScript.rtilt, moveScript.atilt_phase, moveScript.atilt_severity, 
										moveScript.angle, moveScript.center);
		Destroy(gameObject); //Destroy object this script is attatched to
	}

	void Death() {
		asteroidSpawner.asteroidDestroyed(); //Decrease amount of asteroids
		Destroy(gameObject); //Destroy object this script is attatched to
	}
}
