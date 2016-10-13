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
        // Destroy(other.gameObject);
        GameObject gameObject = other.gameObject;

        // get playercontrols script
        PlayerControls script = gameObject.GetComponent<PlayerControls>();

        Debug.Log ("HIT SS");

        script.slowingDown = true;

        // int curspeedofplayer = script.currentSpeed;
        // Debug.Log ("" + curspeedofplayer);

    }
}
