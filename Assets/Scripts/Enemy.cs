using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// 0 is scout type, 1 is cruiser type, 2 is mothership type
	public int type;
	public int hp;
	public int hpMax;

	// 0 is aggressive, 1 is balanced, 2 is defensive.
	public int aggroState;

	// used to determine if bullets are causing us to move in ways we don't want to
	public bool rolling;
	public bool pitching;
	public bool yawing;

	public int speed = 0;
	public int massScaling = 100000;
	public int maxSpeed = 400;
	public int rotationSpeed = 1;

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
		determineAggroState();

		// moveStraight();

		// note that the forward is Z
		curSpeed = rb.velocity.magnitude;

		UpwardsDodgeManeuver();
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
        }

        Debug.Log("Enemy ship hit");
        
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

	public void PitchUp() {
		rb.transform.Rotate(rotationSpeed * -1, 0, 0, Space.Self);
	}

	public void PitchDown() {
		rb.transform.Rotate(rotationSpeed, 0, 0, Space.Self);
	}

	public void YawRight() {
		rb.transform.Rotate(0, rotationSpeed, 0, Space.Self);
	}

	public void YawLeft() {
		rb.transform.Rotate(0, rotationSpeed * -1, 0, Space.Self);
	}

	public void RollLeft() {
		rb.transform.Rotate(0, 0, rotationSpeed, Space.Self);
	}

	public void RollRight() {
		rb.transform.Rotate(0, 0, rotationSpeed * -1, Space.Self);
	}

	// don't need to do speed checks for hop thrust since every time we use it
	// we counterfire the thrust to reset it to zero
	public void HopUp(float thrust) {
		rb.AddForce(transform.up * thrust);
	}

	public void HopDown(float thrust) {
		rb.AddForce(transform.up * thrust * -1);
	}
}
