using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	public static int SHOTDAMAGE = 5;
    
	void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
			Debug.Log("Hit player for: " + SHOTDAMAGE);
            PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth>();
            ph.TakeDamage(SHOTDAMAGE);
			Destroy(this.gameObject);
        }
    }
}
