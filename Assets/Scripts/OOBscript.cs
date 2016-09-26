using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OOBscript : MonoBehaviour {

	public bool danger = false;
	public Text HUDdanger;

	// Use this for initialization
	void Start () {
		HUDdanger = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (danger) {
			HUDdanger.text = "Turn Around!";
		} else {
			HUDdanger.text = "SAFE";
		}
	}
}
