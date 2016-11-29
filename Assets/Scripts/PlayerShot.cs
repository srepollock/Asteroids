using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

	public static int SHOTDAMAGE = 50;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        } else if (col.gameObject.tag == "Asteroid") {
            Destroy(this.gameObject);
        } else if (col.gameObject.tag == "Boss") {
            BossHealth bh = col.gameObject.GetComponent<BossHealth>();
            bh.TakeDamage(SHOTDAMAGE);
            Destroy(this.gameObject);
        }
    }
}
