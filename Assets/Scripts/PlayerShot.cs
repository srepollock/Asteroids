using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

	public static int SHOTDAMAGE = 50;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Asteroid") {
            Debug.Log("Shot hit asteroid");
            Destroy(this.gameObject);
        }
    }

}
