using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// 0 is scout type, 1 is cruiser type, 2 is mothership type
	public int type;
	public int hp;
	public int hpMax;

	// 0 is aggressive, 1 is balanced, 2 is defensive.
	public int aggroState;
    public GameObject shot;
    public Transform shotSpawnTop;
    public Transform shotSpawnBottom;
    public int shotCooldown = 0;

	// used to determine if bullets are causing us to move in ways we don't want to
	public bool rolling;
	public bool pitching;
	public bool yawing;

	public int speed = 0;
	public int massScaling = 100000;
	public int maxSpeed = 400;
	public float rotationSpeed = 0.001f;

	public float curSpeed;

	// used to determine if we're currently doing a maneuver
	public bool inManeuver;
	// keeps track of how long we've been in the maneuver
	public int maneuverCounter; 

	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		if (type == 0) {
			hp = 500;
			hpMax = 500;
		}
		if (type == 1) {
			hp = 50000;
			hpMax = 50000;
		}
		if (type == 2) {
			hp = 1000000;
			hpMax = 1000000;
		}

		rb = GetComponent<Rigidbody>();
	}
	
	// Fixed update is called 200 times per second (1 per physics update)
	void FixedUpdate () {
		// check if we're dead
		if (hp <= 0) {
			// perhaps play an explosion here
			Destroy(this.gameObject);
			return;
		}

		// shot cooldown
		shotCooldown--;

		determineAggroState();

		// do aggrostate stuff here, but for beta ai we just need the aggro ai
		angleTowardsPlayer();
		moveStraight();

		// shoot if roughly pointing at player
		Vector3 targetDir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
		float angle = Vector3.Angle( targetDir, transform.forward );

		if(angle < 5.0f ) {
			shootStraight();
		}

		
		// note that the forward is Z
		curSpeed = rb.velocity.magnitude;

		// testing maneuvers
		// UpwardsDodgeManeuver();
		// RollLeft();
	}

	void OnCollisionEnter(Collision collision) {
		/*
    	// Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        
        // Play a sound if the colliding objects had a big impact.		
        if (collision.relativeVelocity.magnitude > 2)
            audio.Play();
        */

        if (collision.gameObject.tag == "Shot") {
        	Destroy(collision.gameObject);
        	Debug.Log("Enemy ship hit");
        }
        
    }

	public void determineAggroState() {
		if (hp < (hpMax/4)) {
			aggroState = 2;
		} else if (hp < (hpMax/2)) {
			aggroState = 1;
		} else {
			aggroState = 0;
		}
	}

	// note that we need to factor in the huge mass i put on the ship. I put huge mass in order to prevent bullets from skewing
	// movement of the ship when the bullets hit
	public void moveStraight() {
		// Debug.Log("Enemy ship moving");
		ForwardsThrust(massScaling * speed);
	}

	public void UpwardsDodgeManeuver() {
		if (!inManeuver) {
			maneuverCounter = 0;
			inManeuver = true;
			Debug.Log("Maneuver started");
		}
		if (inManeuver) {
			if (maneuverCounter < 800) {
				HopUp(massScaling * speed);
				moveStraight();
			} else if (maneuverCounter < 2400) {
				HopDown(massScaling * speed);
				moveStraight();
			} else if (maneuverCounter < 3200) {
				HopUp(massScaling * speed);
				moveStraight();
			} else {
				inManeuver = false;
				moveStraight();
				Debug.Log("maneuver done");
			}
			maneuverCounter++;
		}
	}



	public void CircularDodging() {

	}

	public void shootStraight() {
		if (shotCooldown <= 0) {
			GameObject newshot = (GameObject)Instantiate(shot, shotSpawnTop.position, shotSpawnTop.rotation);
			GameObject newshot2 = (GameObject)Instantiate(shot, shotSpawnBottom.position, shotSpawnBottom.rotation);
			shotCooldown = 400;
		}
	}

	public void angleTowardsPlayer() {
		// find where player is
		Transform target = GameObject.FindGameObjectWithTag("Player").transform;

		Vector3 targetDir = target.position - transform.position;
        float step = rotationSpeed;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

	}

	public void ForwardsThrust(float thrust) {
		// if we are under max speed, add speed
		if ((rb.velocity.magnitude < maxSpeed)) {
			rb.AddForce(transform.forward * thrust);
		}
		
	}

	public void ReverseThrust(float thrust) {
		rb.AddForce(transform.forward * -1 * thrust);
	}

	// don't need to do speed checks for left and right thrust since every time we use left and right thrust 
	// we counterfire the thrust to reset it to zero
	public void LeftThrust(float thrust) {
		rb.AddForce(transform.right * thrust);
	}

	public void RightThrust(float thrust) {
		rb.AddForce(transform.right * -1 * thrust);
	}

	// note that the *30 for the rotations is neccesary otherwise it rotates REALLY REALLY SLOW since the rotation speed is hella low
	// the rotation speed has to be hella low in order for "angle towards player" function to actually angle slowly
	public void PitchUp() {
		rb.transform.Rotate(rotationSpeed * 30 * -1, 0, 0, Space.Self);
	}

	public void PitchDown() {
		rb.transform.Rotate(rotationSpeed * 30, 0, 0, Space.Self);
	}

	public void YawRight() {
		rb.transform.Rotate(0, rotationSpeed * 30, 0, Space.Self);
	}

	public void YawLeft() {
		rb.transform.Rotate(0, rotationSpeed * 30 * -1, 0, Space.Self);
	}

	public void RollLeft() {
		rb.transform.Rotate(0, 0, rotationSpeed * 30, Space.Self);
	}

	public void RollRight() {
		rb.transform.Rotate(0, 0, rotationSpeed * 30 * -1, Space.Self);
	}

	// don't need to do speed checks for hop thrust since every time we use it (only in maneuvers)
	// we counterfire the thrust to reset it to zero
	public void HopUp(float thrust) {
		rb.AddForce(transform.up * thrust);
	}

	public void HopDown(float thrust) {
		rb.AddForce(transform.up * thrust * -1);
	}
}
