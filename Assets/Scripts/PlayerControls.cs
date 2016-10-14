using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public int minSpeed = 10;
	public int maxSpeed = 100;
	private int speedIncrease = 5;
	public int currentSpeed = 10; // Initially is the slowest speed
	public float rotateSpeed = 45.0f;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public bool slowingDown = false;

    public MenuManager menuManager;

	// Use this for initialization
	void Start () {
		
	}
	
    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
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

		// ship slowing down 
		if (slowingDown) {
			ModifySpeed(-speedIncrease * 5);
			if (currentSpeed == minSpeed) {
				currentSpeed = 0;

				// change to shop scene
				menuManager.increaseLevel();
			}
		} else {
			// Spacecraft Acceleration/Deceleration
			if (Input.GetKey (KeyCode.W)) {
				ModifySpeed (speedIncrease);
			} else if (Input.GetKey (KeyCode.S)) {
				ModifySpeed (-speedIncrease);
			}
		}

		// Move forward based to mouse
		Vector3 mousePos = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f));
		transform.Rotate (new Vector3 (-mousePos.y, mousePos.x, -mousePos.x) * 0.025f);
		transform.Translate (Vector3.forward * Time.deltaTime * currentSpeed);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Asteroid") {
			Debug.Log ("hit asteroid");
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Shot") {
			Debug.Log ("hit own shot");
			Destroy (col.gameObject);
		}
	}

	void ModifySpeed(int speed) {
		currentSpeed += speed;
		if (currentSpeed < minSpeed) {
			currentSpeed = minSpeed;
		}
		if (currentSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		}
	}

}
