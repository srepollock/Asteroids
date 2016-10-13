using UnityEngine;
using System.Collections;

public class innerBoundaryCollision : MonoBehaviour {

	private OOBscript oob;
	public GameObject HUD;

	// Use this for initialization
	void Start () {
		oob = HUD.GetComponent<OOBscript> ();
		Debug.Log ("boundary created");
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			oob.danger = false;
			Debug.Log ("Object exited");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			oob.danger = true;
			Debug.Log ("Object entered");
		}
	}
}
