using UnityEngine;
using System.Collections;
using System;

public class DestroyByContact : MonoBehaviour {
    GameObject sancho;
    AsteroidSpawner asteroidSpawner;
	public AudioSource explode;

    Eliptical_movement moveScript;

    // health variables
    enum asteroidTypeHealth : int { Small = 100, Medium = 200, Large = 300 };
    public string asteroidType = "Small";
    private int asteroidMaxHealth = 0;
    private int asteroidHealth = 0;

    void Start()
    {
        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
        moveScript = GetComponent<Eliptical_movement>();

        //Set the health of the asteroid depending on the type
        asteroidMaxHealth = (int)Enum.Parse(typeof(asteroidTypeHealth), asteroidType);
        asteroidHealth = asteroidMaxHealth;
		Debug.Log("asteroid health = " + asteroidHealth);
    }

	void OnTriggerEnter(Collider other) {
        //Destroyed when hit by a player shot
        if (other.tag != "Shot") {
            return;
        } else {
            asteroidHealth -= PlayerPrefs.GetInt("playershotdamage");
            Destroy(other.gameObject); //Destroy object that entered the collider
            Debug.Log("asteroid health = " + asteroidHealth);
            if (asteroidHealth <= 0) {
                
                if (asteroidType == "Small") {
					explode.Play ();
                    Destroy(gameObject); //Destroy object this script is attatched to
                    asteroidSpawner.asteroidDestroyed("Small"); //Decrease amount of asteroids
                    Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);    
                }

				if (asteroidType == "Medium") {
					explode.Play ();
                    Destroy(gameObject); //Destroy object this script is attatched to
                    asteroidSpawner.asteroidDestroyed("Medium"); //Increase amount of asteroids
                    asteroidSpawner.explodeAsteroid("Medium", moveScript.radiusA, moveScript.radiusB, 
                                                    moveScript.speed, moveScript.rtilt, moveScript.atilt_phase, moveScript.atilt_severity, 
                                                    moveScript.angle, moveScript.center);
                    Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);    
                }

				if (asteroidType == "Large") {
					explode.Play ();
                    Destroy(gameObject); //Destroy object this script is attatched to
                    asteroidSpawner.asteroidDestroyed("Large"); //Increase amount of asteroids
                    asteroidSpawner.explodeAsteroid("Large", moveScript.radiusA, moveScript.radiusB, 
                                                    moveScript.speed, moveScript.rtilt, moveScript.atilt_phase, moveScript.atilt_severity, 
                                                    moveScript.angle, moveScript.center);
                    Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);    
                }
            }
        }
    }

    public void setSize(string size) {
        asteroidType = size;
    }

    public int getRemainingAsteroidHealth() {
        return asteroidHealth;
    }
}
