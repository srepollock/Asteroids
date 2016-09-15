using UnityEngine;
using System.Collections;

public class BoundaryCollision : MonoBehaviour {

	private OOBscript oob;
	public GameObject HUD;

	// Use this for initialization
	void Start () {
		oob = HUD.GetComponent<OOBscript> ();
		Debug.Log ("boundary created");
	}

	void OnTriggerStay(Collider other) {
		oob = HUD.GetComponent<OOBscript> ();
		oob.danger = true;
		//Debug.Log ("Object inside");
	}

	void OnTriggerExit(Collider other) {
		oob = HUD.GetComponent<OOBscript> ();
		oob.danger = false;
		Debug.Log ("Object exited");
	}

	void OnTriggerEnter(Collider other) {
		oob = HUD.GetComponent<OOBscript> ();
		oob.danger = true;
		Debug.Log ("Object entered");
	}
}
