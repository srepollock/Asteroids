using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public int minSpeed = 10;
	public int maxSpeed = 100;
	private int speedIncrease = 5;
	public int currentSpeed = 10; // Initially is the slowest speed
	public float rotateSpeed = 45.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// Spacecraft Roll
		if (Input.GetKey (KeyCode.A)) {
			// Rotate about the X axis of the obj
			transform.Rotate (0, 0, Time.deltaTime * rotateSpeed);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0, 0, -Time.deltaTime * rotateSpeed);
		}
		// Spacecraft Acceleration/Deceleration
		if (Input.GetKey (KeyCode.W)) {
			currentSpeed += speedIncrease;
			if (currentSpeed > maxSpeed)
				currentSpeed = maxSpeed;
		} else if (Input.GetKey (KeyCode.S)) {
			currentSpeed -= speedIncrease;
			if (currentSpeed < minSpeed)
				currentSpeed = minSpeed;
		}
		// Move forward based to mouse
		Vector3 mousePos = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f));
		transform.Rotate (new Vector3 (-mousePos.y, mousePos.x, -mousePos.x) * 0.025f);
		transform.Translate (Vector3.forward * Time.deltaTime * currentSpeed);
	}
}
