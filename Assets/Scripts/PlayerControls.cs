using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    // damage variables
    public int SMALL_ASTEROID_DAMAGE = 50;
    public int MEDIUM_ASTEROID_DAMAGE = 20;

    // movement variables
	public int minSpeed = 10;
	public int maxSpeed = 75;
	private int speedIncrease = 5;
	public int currentSpeed = 10; // Initially is the slowest speed
	public float rotateSpeed = 45.0f;
	public bool slowingDown = false;

	public MenuManager menuManager;
    public GameObject shot;
    public Transform shotSpawn;

    // shoooting variables
    public float fireRate = 0.01f;

    // shot deviation variables
    public float deviationIncreaseAmount = 0.1f;
    public float deviationDecreaseAmount = 0.1f;
    private float shotDeviationScale = 1;
    private Time mouseHeldDown;
    private Time mouseUp;
    private bool shooting;
    private float nextFire = 0.0f;
    private float bulletStrayX = 0;
    private float bulletStrayY = 0;
    private float bulletStrayZ = 0;

	private Rect deadzone;

    private GameObject sancho;
    private AsteroidSpawner asteroidSpawner;

    // Use this for initialization
    void Start () {
		Vector2 c = new Vector2 (Screen.width / 2, Screen.height / 2);
		deadzone = new Rect (c.x - 10f, // x pos top left
							 c.y - 10f, // y pos top left
							 40f, // width
							 40f); // height

        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
    }
	
    void Update() {
        if ((Input.GetButton("Fire1")) && (Time.time > nextFire)) {
            nextFire = Time.time + fireRate;
            GameObject newshot = (GameObject) Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            // first shot is perfect straight shot, others will deviate
            if (shooting) {
            	// generate random numbers for stray
	            bulletStrayX = Random.Range((-1 * shotDeviationScale), (1 * shotDeviationScale));
	            bulletStrayY = Random.Range((-1 * shotDeviationScale), (1 * shotDeviationScale));
	            bulletStrayZ = Random.Range((-1 * shotDeviationScale), (1 * shotDeviationScale));

	            newshot.transform.Rotate(bulletStrayX, bulletStrayY, bulletStrayZ);
            }

            shooting = true;
        }

        if (Input.GetButtonUp("Fire1")) {
        	shooting = false;
        }

        if (shooting) {
        	// increase shot deviation
        	shotDeviationScale += deviationIncreaseAmount;
        	if (shotDeviationScale > 5f) {
        		shotDeviationScale = 5;
        	}
        } else {
        	shotDeviationScale -= deviationDecreaseAmount;
        	if (shotDeviationScale < 1) {
        		shotDeviationScale = 1;
        	}
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
		Vector3 mousePos = (Input.mousePosition - (new Vector3 (Screen.width, Screen.height, 0) / 2.0f));
		if (deadzone.Contains (Input.mousePosition)) {
			mousePos = Vector3.zero;
		}
		transform.Rotate (new Vector3 (-mousePos.y, mousePos.x, -mousePos.x) * 0.025f);
		transform.Translate (Vector3.forward * Time.deltaTime * currentSpeed);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Asteroid") {
			Debug.Log ("hit asteroid");

			int dmg = col.gameObject.GetComponent<DestroyByContact>().getRemainingAsteroidHealth();

			Destroy (col.gameObject);

            Debug.Log("Player health before collison: " + PlayerPrefs.GetInt("playerhealth"));
            Debug.Log("Taking " + dmg + " Damage");
            PlayerTakeDamage(dmg);
            Debug.Log("Player health after collison: " + PlayerPrefs.GetInt("playerhealth"));
            asteroidSpawner.asteroidDestroyed();
            Debug.Log("curAsteroids = " + asteroidSpawner.curAsteroids);
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

    void PlayerTakeDamage(int damage) {
        PlayerPrefs.SetInt("playerhealth", PlayerPrefs.GetInt("playerhealth") - damage);
        if(PlayerPrefs.GetInt("playerhealth") <= 0) {
            menuManager.goToScene("end_screen");
        }
    }
}
