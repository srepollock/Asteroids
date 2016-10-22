using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
    GameObject sancho;
    AsteroidSpawner asteroidSpawner;

    void Start()
    {
        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
    }

	void OnTriggerEnter(Collider other) {
        if(other.tag != "Shot") {
            return;
        }
        Destroy(other.gameObject); //Destroy object that entered the collider
        Destroy(gameObject); //Destroy object this script is attatched to
        asteroidSpawner.asteroidDestroyed(); //Decrement amount of asteroids
        Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);
    }
}
