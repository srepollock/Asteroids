using UnityEngine;
using System.Collections;

public class SmallAsteroid : MonoBehaviour {

	public static int ASTEROIDDAMAGE = 100;
	public static int ASTEROIDHEALTH = 100;
	public static int ASTEROIDSCORE = 10;

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
			playerHealth.TakeDamage(currentHealth);
			Death();
		}
		if (col.gameObject.tag == "Shot") {
			TakeDamage(PlayerShot.SHOTDAMAGE);
			if (currentHealth <= 0 && !isDead) {
				isDead = true;
				Death();
			}
		}
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}

	void Death() {
		asteroidSpawner.asteroidDestroyed(); //Decrease amount of asteroids
		AddScore();
		Destroy(gameObject); //Destroy object this script is attatched to
	}

	void AddScore() {
		PlayerScore ps = GameObject.FindGameObjectWithTag("Player")
			.GetComponent<PlayerScore>();
		ps.AddScoreAndCurrency(ASTEROIDSCORE);
	}
}
