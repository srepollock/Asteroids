using UnityEngine;
using System.Collections;

public class EnterSpaceStation : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Shot") {
			Debug.Log ("SS hit shot");
			Destroy (other.gameObject);
		} else {
	        GameObject gameObject = other.gameObject;
            GameObject sancho = GameObject.Find("Sancho");

            // get playercontrols script
            PlayerControls script = gameObject.GetComponent<PlayerControls>();
            AsteroidSpawner asteroidSpawnerScript = sancho.GetComponent<AsteroidSpawner>();

            if (asteroidSpawnerScript.curAsteroids == 0)
            {
                if (script != null)
                {
                    script.slowingDown = true;
                }
            }
		}
    }
}
