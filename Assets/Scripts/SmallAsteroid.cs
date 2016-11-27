using UnityEngine;
using System.Collections;

public class SmallAsteroid : MonoBehaviour {

	public static int ASTEROIDDAMAGE = 100;
	public static int ASTEROIDHEALTH = 100;

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
			PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(ASTEROIDDAMAGE - currentHealth);
			Death();
		}
		if (col.gameObject.tag == "Shot") {
			TakeDamage(PlayerShot.SHOTDAMAGE);
			if (currentHealth <= 0) {
				Debug.Log("Asteroid destroyed");
				Death();
			}
		}
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}

	void Death() {
		Destroy(gameObject); //Destroy object this script is attatched to
		asteroidSpawner.asteroidDestroyed("Small"); //Decrease amount of asteroids
		Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);
	}
}
