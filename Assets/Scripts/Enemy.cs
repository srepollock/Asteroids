using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// 0 is scout type, 1 is cruiser type, 2 is mothership type
	public int type;
	public int hp;

	public int aggroState;

	// Use this for initialization
	void Start () {
		if (type == 0) {
			hp = 500;
		}
		if (type == 1) {
			hp = 50000;
		}
		if (type == 2) {
			hp = 1000000;
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void UpwardsDodgeManeuver() {

	}

	public void CircularDodging() {

	}

	public void ForwardsThrust() {

	}

	public void ReverseThrust() {

	}

	public void LeftThrust() {

	}

	public void RightThrust() {

	}

	public void PitchUp() {

	}

	public void PitchDown() {

	}

	public void YawRight() {

	}

	public void YawLeft() {

	}

	public void RollLeft() {

	}

	public void RollRight() {

	}

	public void HopUp() {

	}

	public void HopDown() {

	}
}
