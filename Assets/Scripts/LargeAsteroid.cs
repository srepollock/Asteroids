using UnityEngine;
using System.Collections;

public class LargeAsteroid : MonoBehaviour {

	public static int ASTEROIDDAMAGE = MediumAsteroid.ASTEROIDDAMAGE * 4 + SmallAsteroid.ASTEROIDDAMAGE * 16 + ASTEROIDHEALTH;
	public static int ASTEROIDHEALTH = 400;
	public static int ASTEROIDSCORE = 40;

    public GameObject explosion;

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
				SpawnMedium();
			}
		}
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}

	void SpawnMedium() {
		Destroy(gameObject); //Destroy object this script is attatched to
		asteroidSpawner.explodeAsteroid("Large", moveScript.radiusA, moveScript.radiusB, 
										moveScript.speed, moveScript.rtilt, moveScript.atilt_phase, moveScript.atilt_severity, 
										moveScript.angle, moveScript.center);
		asteroidSpawner.asteroidDestroyed(); //Increase amount of asteroids  
	}

	void Death(){
		asteroidSpawner.asteroidDestroyed(); //Decrease amount of asteroids
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(gameObject); //Destroy object this script is attatched to
	}
}
