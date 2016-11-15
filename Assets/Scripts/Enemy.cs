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
	
	// Update is called once per frame
	void Update () {
		determineAggroState();
		// check if we're rolling, yawing, or pitching on purpose
		// if we're not doing it on purpose, straighten fix it.
		straightenup();
	}

	// should be called because there's a rigid body on the prefab.
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
        Debug.Log("Enemy ship hit");
        
    }

    public void straightenup() {
    	if (!yawing) {

    	}

    	if (!rolling) {

    	}

    	if (!pitching) {

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

	public void UpwardsDodgeManeuver() {

	}

	public void CircularDodging() {

	}

	public void ForwardsThrust(float thrust) {
		rb.AddForce(transform.forward * thrust);
	}

	public void ReverseThrust(float thrust) {
		rb.AddForce(transform.forward * -1 * thrust);
	}

	public void LeftThrust(float thrust) {

	}

	public void RightThrust(float thrust) {

	}

	public void PitchUp(float thrust) {

	}

	public void PitchDown(float thrust) {

	}

	public void YawRight(float thrust) {

	}

	public void YawLeft(float thrust) {

	}

	public void RollLeft(float thrust) {

	}

	public void RollRight(float thrust) {

	}

	public void HopUp(float thrust) {

	}

	public void HopDown(float thrust) {

	}
}
