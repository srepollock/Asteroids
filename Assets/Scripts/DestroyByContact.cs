using UnityEngine;
using System.Collections;
using System;



public class DestroyByContact : MonoBehaviour {
    GameObject sancho;
    AsteroidSpawner asteroidSpawner;

    // health variables
    enum asteroidTypeHealth : int { Small = 100, Medium = 200, Large = 300 };
    public string asteroidType = "Small";
    private int asteroidMaxHealth = 0;
    private int asteroidHealth = 0;

    void Start()
    {
        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();

        //Set the health of the asteroid depending on the type
        asteroidMaxHealth = (int)Enum.Parse(typeof(asteroidTypeHealth), asteroidType);
        asteroidHealth = asteroidMaxHealth;
        Debug.Log(asteroidHealth);
    }

	void OnTriggerEnter(Collider other) {
        //Destroyed when hit by a player shot
        if (other.tag != "Shot") {
            return;
        } else {
            asteroidHealth -= PlayerPrefs.GetInt("playershotdamage");
            Destroy(other.gameObject); //Destroy object that entered the collider
            Debug.Log(asteroidHealth);
            if (asteroidHealth <= 0) {
                Destroy(gameObject); //Destroy object this script is attatched to
                asteroidSpawner.asteroidDestroyed(); //Decrement amount of asteroids
                Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);
            }
        }
    }
}
