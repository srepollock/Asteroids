using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if(other.tag != "Shot") {
            return;
        }
        Destroy(other.gameObject); //Destroy object that entered the collider
        Destroy(gameObject); //Destroy object this script is attatched to
    }
}
